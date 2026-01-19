using System.Net;
using System.Text.Json;
using LMS.Application.Dtos.Responses;
using static LMS.Application.Helpers.CustomExceptions;

namespace LibraryManagementSystem.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlingMiddleware(RequestDelegate next,
           ILogger<ExceptionHandlingMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

                await HandleExceptionAsync(context, ex, _environment.IsDevelopment());
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            bool isDevelopment)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var errorResponse = new ErrorResponse();
            switch (exception)
            {
                case ValidationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message ?? "Validation failed";
                    break;

                case NotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = ex.Message ?? "Resource not found";
                    break;

                case UnauthorizedException ex:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.Message = ex.Message ?? "Unauthorized access";
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "An internal server error occurred. Please try again later! ";
                    break;
            }


            if (isDevelopment || exception.InnerException != null)
            {
                errorResponse.Message = exception.Message;
                errorResponse.InnerException = exception.InnerException?.Message;
                errorResponse.StackTrace = exception.StackTrace;
            }

            var apiResponse = ApiResponse<ErrorResponse>.Fail(errorResponse.Message, errorResponse);

            var jsonResponse = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = isDevelopment
            });

            await response.WriteAsync(jsonResponse);
        }
    }
}
