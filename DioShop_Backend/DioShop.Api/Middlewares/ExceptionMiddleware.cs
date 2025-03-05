using DioShop.Application.Exceptions;
using DioShop.Application.Ultils;
using Newtonsoft.Json;
using System.Net;

namespace DioShop.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                //// Kiểm tra lỗi Authorization
                if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    //await HandleAuthorizationError(httpContext, "You are not authorized to access this resource.");
                    throw new Exception("You are not authorized to access this resource.Please log in to continue");
                }
                else if (httpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    // await HandleAuthorizationError(httpContext, "You do not have permission to access this resource.");
                    throw new Exception("You do not have permission to access this resource.");
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errorType = "Failure";
            //var errorMessage = "An unexpected error occurred.";
            var errorMessage = exception.Message;
            if (exception is ValidationException validationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = JsonConvert.SerializeObject(validationException.Errors);
            }
            else if (exception is NotFoundException notFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                errorMessage = notFoundException.Message;
            }
            else if (exception is UnauthorizedException unauthorizedException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                errorMessage = unauthorizedException.Message;
            }

            var response = new ApiResponse<object>
            {
                Success = false,
                Message = errorMessage
            };
            //// Định dạng phản hồi JSON
            //var response = new ApiResponse<object>
            //{
            //    Success = false,
            //    Message = errorMessage
            //};

            var result = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }
    }

    public class ErrorDetails
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}
