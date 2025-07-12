using Microsoft.Extensions.Hosting;
using Serilog;

namespace ObservabilityKit.Sentry;

public static class LoggingConfiguration
{
    public static void UseLogging(this IHostBuilder builder)
    {
        UseLogging(builder, _ => { });
    }

    public static void UseLogging(
        this IHostBuilder builder,
        Action<LoggingOptions.Builder> configure)
    {
        var optionsBuilder = new LoggingOptions.Builder();
        configure(optionsBuilder);
        var options = optionsBuilder.Build();

        builder.UseSerilog((_, loggerConfiguration) =>
        {
            loggerConfiguration
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    restrictedToMinimumLevel: options.ConsoleMinimumEventLevel);

            if (options.SentryDsn is not null)
            {
                loggerConfiguration.WriteTo.Sentry(
                    dsn: options.SentryDsn.ToString(),
                    environment: options.AspNetCoreEnvironment,
                    debug: options.SentryDebug,
                    minimumEventLevel: options.SentryMinimumEventLogLevel,
                    minimumBreadcrumbLevel: options.SentryMinimumBreadcrumbLogLevel,
                    defaultTags: options.Tags);
            }
        });
    }
}