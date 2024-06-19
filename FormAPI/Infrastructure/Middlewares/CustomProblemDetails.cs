namespace FormAPI.Infrastructure.Middlewares
{
    /// <summary>
    /// Stores exception details
    /// </summary>
    public class CustomProblemDetails(string errorMessage, int statusCode)
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; } = errorMessage;

        /// <summary>
        /// Status code
        /// </summary>
        public int StatusCode { get; } = statusCode;
    }
}
