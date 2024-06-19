using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.Infrastructure.Middlewares
{

    /// <summary>
    /// Handles exception globally
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="requestDelegate">Handles http request</param>
    public class ExceptionMiddleware(RequestDelegate requestDelegate)
    {
        /// <summary>
        /// Handles http request
        /// </summary>
        private readonly RequestDelegate _requestDelegate = requestDelegate;

        /// <summary>
        /// Triggers when a request has been made and handles exceptions if any
        /// </summary>
        /// <param name="httpContext">Includes information about http request</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (CosmosException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handles the error messages being displayed
        /// </summary>
        /// <param name="context">Includes information about http request</param>
        /// <param name="exception">Exception thrown</param>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = GetError(exception);

            var responseObject = new ObjectResult(error.ErrorMessage)
            {
                StatusCode = error.StatusCode,
                
            };

            var result = JsonConvert.SerializeObject(responseObject);

            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = error.StatusCode;
            return context.Response.WriteAsync(result);
        }

        /// <summary>
        /// Get error details
        /// </summary>
        /// <param name="exception">Exception thrown</param>
        /// <returns></returns>
        private CustomProblemDetails GetError(Exception exception)
        {
            if (exception is ValidationException v)
            {
                return new CustomProblemDetails(v.ValidationResult.ErrorMessage, StatusCodes.Status400BadRequest);
            }

            return new CustomProblemDetails(exception.Message,StatusCodes.Status500InternalServerError);
        }
    }
}
