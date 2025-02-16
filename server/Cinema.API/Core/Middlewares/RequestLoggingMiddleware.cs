using System.Text.Json;

namespace Cinema.API.Core.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        var bodyAsText = await ReadRequestBodyAsync(context);
        var bodyAsJson = DeserializeRequestBody(bodyAsText);
        var logInfo = CreateLogInfo(context, bodyAsJson);
        LogRequest(logInfo);

        await _next(context);
    }

    private static async Task<string> ReadRequestBodyAsync(HttpContext context)
    {
        using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
        var bodyAsText = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;
        return bodyAsText;
    }

    private static object? DeserializeRequestBody(string bodyAsText)
    {
        if (string.IsNullOrWhiteSpace(bodyAsText))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<object>(bodyAsText);
        }
        catch (JsonException)
        {
            return bodyAsText;
        }
    }

    private static object CreateLogInfo(HttpContext context, object bodyAsJson)
    {
        return new
        {
            context.Request.Method,
            Path = context.Request.Path.ToString(),
            QueryParameters = context.Request.QueryString,
            Body = bodyAsJson,
            context.Request.Query,
            context.Request.ContentType,
            context.Request.ContentLength,
        };
    }

    private void LogRequest(object logInfo)
    {
        var logJson = JsonSerializer.Serialize(logInfo, GetJsonSerializerOptions());
        _logger.LogInformation(logJson);
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true
        };
    }
}