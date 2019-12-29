using System;
using System.Runtime.Serialization;

namespace Id3.Frames
{
    [Serializable]
    internal class InvalidCopyrightFrameException : Exception
    {
        public InvalidCopyrightFrameException()
        {
        }

        public InvalidCopyrightFrameException(string message) : base(message)
        {
        }

        public InvalidCopyrightFrameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCopyrightFrameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}