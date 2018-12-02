using System;
using System.Net;

namespace CheckoutChallenge.Client
{
    public class OrderingClientException : Exception
    {
        public OrderingClientException(string message)
            : this(message, null)
        {
        }

        public OrderingClientException(string message, HttpStatusCode statusCode)
            : this(message, statusCode, null)
        {
        }

        public OrderingClientException(string message, Exception innerException)
            : this(message, 0, innerException)
        {
        }

        public OrderingClientException(string message, HttpStatusCode statusCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
