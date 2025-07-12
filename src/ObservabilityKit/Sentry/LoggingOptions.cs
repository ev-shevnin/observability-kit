using Serilog.Events;

namespace ObservabilityKit.Sentry;

public class LoggingOptions
{
    public string? AspNetCoreEnvironment { get; set; }
    public Uri? SentryDsn { get; set; }
    public Dictionary<string, string>? Tags { get; set; }
    public LogEventLevel ConsoleMinimumEventLevel { get; set; } = LogEventLevel.Information;
    public LogEventLevel SentryMinimumEventLogLevel { get; set; } = LogEventLevel.Error;
    public LogEventLevel SentryMinimumBreadcrumbLogLevel { get; set; } = LogEventLevel.Error;
    public bool SentryDebug { get; set; } = false;
    
    private LoggingOptions() { }

    public class Builder
    {
        private readonly LoggingOptions _options = new();

        public Builder SetAspNetCoreEnvironment(string environment)
        {
            _options.AspNetCoreEnvironment = environment;
            return this;
        }

        public Builder SetSentryDsn(string dsn)
        {
            _options.SentryDsn = new Uri(dsn);
            return this;
        }

        public Builder SetTags(Dictionary<string, string> tags)
        {
            _options.Tags = tags;
            return this;
        }

        public Builder SetConsoleMinimumEventLevel(LogEventLevel level)
        {
            _options.ConsoleMinimumEventLevel = level;
            return this;
        }

        public Builder SetSentryMinimumEventLogLevel(LogEventLevel level)
        {
            _options.SentryMinimumEventLogLevel = level;
            return this;
        }

        public Builder SetSentryMinimumBreadcrumbLogLevel(LogEventLevel level)
        {
            _options.SentryMinimumBreadcrumbLogLevel = level;
            return this;
        }

        public Builder SetSentryDebug(bool debug)
        {
            _options.SentryDebug = debug;
            return this;
        }

        internal LoggingOptions Build() => _options;
    }
}