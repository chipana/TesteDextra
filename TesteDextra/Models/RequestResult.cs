using System.Net;

namespace TesteDextra.Models
{
    /// <summary>
    /// Request result
    /// </summary>
    public class RequestResult
    {
        /// <summary>
        /// Result code
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Result Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RequestResult() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public RequestResult(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
