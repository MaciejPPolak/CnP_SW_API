using System.Net;

namespace SW_API.Domain.Entities
{
    /// <summary>
    /// Error message returned by requested operation
    /// </summary>
    public class Error
    {
        /// <summary>
        /// HTTP Status Code
        /// </summary>
        public HttpStatusCode ErrorCode;
        /// <summary>
        /// String with detailed information
        /// </summary>
        public string ReasonMessage;

        /// <summary>
        /// Generate new error object
        /// </summary>
        public Error(HttpStatusCode code, string message)
        {
            ErrorCode = code;
            ReasonMessage = message;
        }
    }
}
