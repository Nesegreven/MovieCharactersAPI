using System.Net;
using System.Text.Json;
using MovieCharactersAPI.Exceptions;

namespace MovieCharactersAPI.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions globally across the application
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the ErrorHandlingMiddleware
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        /// <param name="logger">The logger instance for error logging</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Processes an HTTP request and handles any exceptions that occur
        /// </summary>
        /// <param name="context">The HTTP context for the current request</param>
        /// <returns>A task that represents the asynchronous operation</returns>
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

        /// <summary>
        /// Handles exceptions by generating appropriate HTTP responses
        /// </summary>
        /// <param name="context">The HTTP context for the current request</param>
        /// <param name="exception">The exception that was thrown</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;

            // Determine the appropriate HTTP status code and message based on the exception type
            switch (exception)
            {
                case NotFoundException notFoundException:
                    status = HttpStatusCode.NotFound; // 404
                    message = notFoundException.Message;
                    break;

                case ConflictException conflictException:
                    status = HttpStatusCode.Conflict; // 409
                    message = conflictException.Message;
                    break;

                case CustomValidationException validationException:
                    status = HttpStatusCode.BadRequest; // 400
                    message = validationException.Message;
                    break;

                case ForbiddenException forbiddenException:
                    status = HttpStatusCode.Forbidden; // 403
                    message = forbiddenException.Message;
                    break;

                case ArgumentException argumentException:
                    status = HttpStatusCode.BadRequest; // 400
                    message = argumentException.Message;
                    break;

                default:
                    // For unhandled exceptions, return a generic error message and log the details
                    status = HttpStatusCode.InternalServerError; // 500
                    message = "An unexpected error occurred.";
                    _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
                    break;
            }

            // Configure the HTTP response
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            // Create the error response object
            var errorResponse = new
            {
                error = message,
                statusCode = (int)status,
                timestamp = DateTime.UtcNow,
                path = context.Request.Path
            };

            // Serialize and send the error response
            var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(result);
        }
    }
}