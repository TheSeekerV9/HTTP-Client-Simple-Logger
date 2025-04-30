using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HttpClientLogger.SimpleHttpClientLogger
{
    public static class LoggingExtensions
    {
        public static IHttpClientBuilder ReplaceLogger(this IHttpClientBuilder builder)
        {
            builder.Services.TryAddScoped<SimpleHttpClientLogger>();
            return builder.RemoveAllLoggers().AddLogger<SimpleHttpClientLogger>(wrapHandlersPipeline: true);
        }
    }
}
