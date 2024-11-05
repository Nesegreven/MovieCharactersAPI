using System.Net;
using System.Text.Json;
using MovieCharactersAPI.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions globally
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    status = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    break;

                case ConflictException conflictException:
                    status = HttpStatusCode.Conflict;
                    message = conflictException.Message;
                    break;

                case CustomValidationException customValidationException:  // Updated
                    status = HttpStatusCode.BadRequest;
                    message = customValidationException.Message;
                    break;

                case ValidationException validationException:  // System.ComponentModel.DataAnnotations
                    status = HttpStatusCode.BadRequest;
                    message = validationException.Message;
                    break;

                case ForbiddenException forbiddenException:
                    status = HttpStatusCode.Forbidden;
                    message = forbiddenException.Message;
                    break;

                case ArgumentException argumentException:
                    status = HttpStatusCode.BadRequest;
                    message = argumentException.Message;
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    _logger.LogError(exception, exception.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var result = JsonSerializer.Serialize(new
            {
                error = message,
                status = (int)status,
                timestamp = DateTime.UtcNow
            });

            await context.Response.WriteAsync(result);
        }
    }
}