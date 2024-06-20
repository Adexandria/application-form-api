using FormAPI.Infrastructure.Utility;

namespace FormAPI.Infrastructure.Services
{
    /// <summary>
    /// Handles response result for all services
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// Succesfully operation
        /// </summary>
        /// <param name="statusCode">Status code</param>
        /// <returns>Response result</returns>
        public ResponseResult SuccessfulOperation(int statusCode = StatusCodes.Status200OK)
        {
            return ResponseResult.SuccessfulOperation(statusCode);
        }

        /// <summary>
        /// Failed operation
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <param name="statusCode">Status code</param>
        /// <returns>Response result</returns>
        public ResponseResult FailedOperation(string errorMessage, int statusCode = StatusCodes.Status400BadRequest)
        {
            return ResponseResult.FailedOperation(errorMessage, statusCode);
        }

        /// <summary>
        /// Successful Operation with response data
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="data">Response data</param>
        /// <returns>Response result</returns>
        public ResponseResult<T> SuccesfulOperation<T>(T data)
        {
            return ResponseResult<T>.SuccessfulOperation(data);
        }


        /// <summary>
        /// Failed operation with response data
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="errorMessage">Error message</param>
        /// <param name="statusCode">Status code</param>
        /// <returns>Response result</returns>
        public ResponseResult<T> FailedOperation<T>(string errorMessage, int statusCode = StatusCodes.Status400BadRequest)
        {
            return ResponseResult<T>.FailedOperation(errorMessage, statusCode);
        }
    }
}
