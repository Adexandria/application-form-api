namespace FormAPI.Infrastructure.Utility
{
    /// <summary>
    /// Response result to handle responses
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// Constructor
        /// </summary>
        internal ResponseResult() 
        {
        }

        /// <summary>
        /// Adds error to an existing list
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns>Response result</returns>
        public ResponseResult AddError(string error)
        {
            Errors.Add(error);
            return this;
        }


        /// <summary>
        /// Add errors to an existing list
        /// </summary>
        /// <param name="errors">List of errors</param>
        /// <returns>Response result</returns>
        public ResponseResult AddErrors(List<string> errors)
        {
            Errors.AddRange(errors);
            return this;
        }

        /// <summary>
        /// Failed operation
        /// </summary>
        /// <param name="error">error message</param>
        /// <param name="statusCode">Status code</param>
        /// <returns>Response result</returns>
        public static ResponseResult FailedOperation(string error, int statusCode = StatusCodes.Status400BadRequest)
        {
            return new ResponseResult()
            {
                Errors = [error],
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// Successful operation
        /// </summary>
        /// <param name="statusCode">Status code</param>
        /// <returns>Response result</returns>
        public static ResponseResult SuccessfulOperation(int statusCode)
        {
            return new ResponseResult()
            {
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// A list of error messages
        /// </summary>
        public List<string> Errors { get;  set; }

        /// <summary>
        /// Status code
        /// </summary>
        public int StatusCode { get;  set; }
    }

    /// <summary>
    /// Handles response result
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    public class ResponseResult<T>() : ResponseResult()
    {

        /// <summary>
        /// Successful operation with response data
        /// </summary>
        /// <param name="Data">Response data</param>
        /// <returns>response result</returns>
        public static ResponseResult<T> SuccessfulOperation(T Data)
        {
            return new ResponseResult<T>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = Data
            };

        }

        /// <summary>
        /// Failed operation with response data
        /// </summary>
        /// <param name="error">Error message</param>
        /// <param name="statusCode">Status code</param>
        /// <returns>Response result</returns>
        public new static ResponseResult<T> FailedOperation(string error, int statusCode = StatusCodes.Status400BadRequest)
        {
            return new ResponseResult<T>()
            {
                Errors = [error],
                StatusCode = statusCode
            };
        }
        /// <summary>
        /// Response data
        /// </summary>
        public T Data { get; set; }
    }
}
