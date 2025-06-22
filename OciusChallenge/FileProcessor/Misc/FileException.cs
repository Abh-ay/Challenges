using System.Net;

namespace FileProcessor.Misc
{
    public class FileException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public FileException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
