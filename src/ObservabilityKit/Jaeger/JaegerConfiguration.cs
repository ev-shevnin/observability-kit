using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ObservabilityKit.Jaeger;

public static class JaegerConfiguration
{
    public static void UseJaeger(this IServiceCollection service)
    {
        UseJaeger(service, _ => { });
    }
    
    public static void UseJaeger(
        this IServiceCollection collection,
        Action<JaegerOptions.Builder> configure)
    {
        var optionsBuilder = new JaegerOptions.Builder();
        configure(optionsBuilder);
        var options = optionsBuilder.Build();
        
        if (!string.IsNullOrEmpty(options.ServiceName) && options.JaegerUrl is not null)
        {
            collection.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(options.ServiceName))
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation()
                        .AddOtlpExporter(otlpOptions => { otlpOptions.Endpoint = options.JaegerUrl; });
                });
        }
    }
}