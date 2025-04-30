# HTTP Client Simple Logger

A clean, simple HTTP client logger for .NET that provides concise request/response logging while suppressing the default verbose logging from  `HttpClient`.

## Features

-   ✅  **Single-line logging**  per HTTP request
    
-   ✅  **Structured logging**  with key request/response metrics
    
-   ✅  **Suppresses default HttpClient logging**  (no more 4+ lines per request)
    
-   ✅  **Easy integration**  with  `IHttpClientFactory`
    
-   ✅  **Error logging**  for failed requests
    
-   ✅  **Request timing**  included in logs

## Usage

### Basic Setup

    services.AddHttpClient("MyClient")
        .ReplaceLogger();

This will configure the client with clean logging like:

    [12:34:56 INF] Processed HTTP request GET https://api.example.com/users status - 200 after 42.5ms

## Log Format

The logger produces messages in this format:

    Processed HTTP request {Method} {Url} status - {StatusCode} after {ElapsedMilliseconds}ms

Where:

-   `{Method}`: HTTP method (GET, POST, etc.)
    
-   `{Url}`: Full request URL
    
-   `{StatusCode}`: HTTP status code (200, 404, etc.)
    
-   `{ElapsedMilliseconds}`: Request duration in milliseconds
    

For failed requests:

    Processed HTTP request {Method} {Url} status - failed after {ElapsedMilliseconds}ms
