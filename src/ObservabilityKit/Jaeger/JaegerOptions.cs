namespace ObservabilityKit.Jaeger;

public class JaegerOptions
{
    public string ServiceName { get; set; } = string.Empty;
    public Uri? JaegerUrl { get; set; }

    private JaegerOptions() { }
    
    public class Builder
    {
        private readonly JaegerOptions _options = new();

        public Builder SetServiceName(string serviceName)
        {
            _options.ServiceName = serviceName;
            return this;
        }

        public Builder SetJaegerUrl(Uri jaegerUrl)
        {
            _options.JaegerUrl = jaegerUrl;
            return this;
        }
        
        internal JaegerOptions Build() => _options;
    }
}