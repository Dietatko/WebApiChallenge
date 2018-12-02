using System;
using System.Runtime.Serialization;

namespace CheckoutChallenge.Domain.Model
{
    public class DataValidationException : Exception
    {
        public DataValidationException()
        {
        }

        public DataValidationException(string message) : base(message)
        {
        }

        public DataValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}