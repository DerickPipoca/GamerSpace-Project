using System.Net;
using System.Text.Json;
using FluentValidation;

namespace GamerSpace.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errorTitle = "An unexpected error occurred.";
            object? errorDetails = null;

            switch (ex)
            {
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorTitle = "Resource not found.";
                    break;

                case InvalidOperationException:
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorTitle = "Invalid request.";
                    break;

                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorTitle = "Validation Failed.";
                    errorDetails = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                    break;

                default:
                    _logger.LogError(ex, "An unhandled exception has occurred.");
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;

            var responseBody = new
            {
                title = errorTitle,
                status = (int)statusCode,
                detail = ex.Message,
                errors = errorDetails
            };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(responseBody));
        }
    }
}