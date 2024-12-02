using System.ComponentModel.DataAnnotations;
using System.Net;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Core.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning(e, "Resource not found: {Message}", e.Message);
            await HandleExceptionAsync(context, HttpStatusCode.NotFound, "Not found", e.Message);
        }
        catch (AppException e)
        {
            _logger.LogError(e, "Application error: {Message}", e.Message);
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error",
                "An application error occurred.");
        }
        catch (ArgumentException e)
        {
            _logger.LogError("Argument error: {Message}", e.Message);
            await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Bad Request", e.Message);
        }
        catch (ValidationException e)
        {
            _logger.LogError("Validation error: {Message}", e.Message);
            await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Bad Request", e.Message);
        }
        catch (DuplicateEntityException e)
        {
            _logger.LogError("Duplicate entity error: {Message}", e.Message);
            await HandleExceptionAsync(context, HttpStatusCode.Conflict, "Conflict", e.Message);
        }
        catch (EntityReferenceViolationException e)
        {
            _logger.LogError("Entity reference violation error: {Message}", e.Message);
            await HandleExceptionAsync(context, HttpStatusCode.Conflict, "Conflict", e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error",
                "An unexpected error occurred. Please try again later.");
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string title,
        string message)
    {
        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = title,
            Detail = message,
            Instance = context.Request.Path.ToString()
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsJsonAsync(problemDetails);
    }
}