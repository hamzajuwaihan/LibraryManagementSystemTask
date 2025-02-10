using System.Net;
using System.Text.Json;
using LibraryManagementSystem.Domain.Shared.Exceptions;

namespace LibraryManagementSystem.Api.Middlewares;

/// <summary>
/// Middleware for handling exceptions globally within the application.
/// </summary>
/// <param name="next">The next middleware in the request pipeline.</param>
/// <param name="logger">The logger instance for logging exceptions.</param>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    /// <summary>
    /// The next middleware in the request pipeline.
    /// </summary>
    private readonly RequestDelegate _next = next;

    /// <summary>
    /// Logger instance for recording exception details.
    /// </summary>
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    /// <summary>
    /// Invokes the middleware to handle exceptions asynchronously.
    /// </summary>
    /// <param name="context">The HTTP context of the current request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles exceptions and sends a formatted JSON response with an appropriate HTTP status code.
    /// </summary>
    /// <param name="context">The HTTP context of the request.</param>
    /// <param name="exception">The caught exception.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        string message;

        if (exception is ApiException ex)
        {
            status = ex.StatusCode;
            message = ex.Message;
        }
        else
        {
            status = HttpStatusCode.InternalServerError;
            message = "An unexpected error occurred.";
        }

        var errorResponse = new
        {
            Message = message,
        };

        string responsePayload = JsonSerializer.Serialize(errorResponse);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(responsePayload);
    }
}
