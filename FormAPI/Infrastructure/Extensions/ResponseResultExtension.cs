using FormAPI.Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;

namespace FormAPI.Infrastructure.Extensions
{
    /// <summary>
    /// Map Custom response result to mvc action result
    /// </summary>
    public static class ResponseResultExtension
    {
        /// <summary>
        /// Maps response to mvc
        /// </summary>
        /// <param name="response">Handles response result</param>
        /// <returns>Action result</returns>
        public static ActionResult Response(this ResponseResult response)
        {
            return response.StatusCode switch 
            {
                StatusCodes.Status200OK => new OkResult(),
                StatusCodes.Status201Created=> new CreatedResult(),
                StatusCodes.Status404NotFound => new NotFoundObjectResult(response.Errors),
                _ => new BadRequestObjectResult(response.Errors)
            };
        }


        /// <summary>
        /// Maps response to mvc
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="response">Handles response result</param>
        /// <returns>Action result</returns>
        public static ActionResult Response<T>(this ResponseResult<T> response)
        {
            return response.StatusCode switch
            {
                StatusCodes.Status200OK => new OkObjectResult(response.Data),
                StatusCodes.Status201Created => new CreatedResult(),
                StatusCodes.Status404NotFound => new NotFoundObjectResult(response.Errors),
                _ => new BadRequestObjectResult(response.Errors)
            };
        }
    }
}
