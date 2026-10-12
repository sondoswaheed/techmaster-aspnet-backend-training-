
using System.Net;
using System.Text.Json;
using task_02_requirements_to_erd.Common.Responses;

namespace task_02_requirements_to_erd.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError( ex,"Unhandled exception occurred. TraceId: {TraceId}", context.TraceIdentifier);

            if (context.Response.HasStarted)
                throw;

            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.ErrorResponse( "An unexpected error occurred. Please try again later." );

            response.Errors.Add( $"Reference ID: {context.TraceIdentifier}");

            await context.Response.WriteAsync( JsonSerializer.Serialize(response) );
        }
    }
}
