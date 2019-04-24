using System;
using System.Runtime.Serialization;

namespace Domain
{
    [Serializable]
    public class NoShowsException : Exception
    {
        public NoShowsException()
        {
        }

        public NoShowsException(string message) : base(message)
        {
        }

        public NoShowsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoShowsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}