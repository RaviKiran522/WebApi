using System.Net;

namespace NZwalks.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> iLogger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> iLogger, RequestDelegate next)
        {
            this.iLogger = iLogger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log this exception
                iLogger.LogError(ex, $"{errorId} : {ex.Message}");

                //retrun custom error response

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are looking into the issue"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
