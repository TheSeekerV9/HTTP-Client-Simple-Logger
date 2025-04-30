using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace HttpClientLogger.SimpleHttpClientLogger
{
    public class SimpleHttpClientLogger : IHttpClientLogger
    {
        private readonly ILogger<SimpleHttpClientLogger> _logger;

        public SimpleHttpClientLogger(ILogger<SimpleHttpClientLogger> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public object LogRequestStart(HttpRequestMessage request)
        {
            return null;
        }

        public void LogRequestStop(
           object context,
           HttpRequestMessage request,
           HttpResponseMessage response,
           TimeSpan elapsed)
        {
            _logger.LogInformation(
                    "Processed HTTP request {Method} {Url} status - {StatusCode} after {ElapsedMilliseconds}ms",
                    request.Method,
                    request.RequestUri,
                    response == null ? 0 : (int)response.StatusCode,
                    elapsed.TotalMilliseconds.ToString("F1")
                );
        }

        public void LogRequestFailed(
            object context,
            HttpRequestMessage request,
            HttpResponseMessage response,
            Exception exception,
            TimeSpan elapsed)
        {
            _logger.LogError(
                exception,
                "Processed HTTP request {Method} {Url} status - failed after {ElapsedMilliseconds}ms",
                    request.Method,
                    request.RequestUri,
                    elapsed.TotalMilliseconds.ToString("F1"));
        }
    }
}
