using System;
using System.Runtime.Serialization;

namespace CheckoutChallenge.Client
{
    public class OrderingClientException : Exception
    {
        public OrderingClientException()
        {
        }

        public OrderingClientException(string message) : base(message)
        {
        }

        public OrderingClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderingClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
