using System;

namespace ValheimPlusManager.Core.Exceptions
{
    public class TryParseException : Exception
    {
        public TryParseException()
        {
        }

        public TryParseException(string message) : base(message)
        {
        }

        public TryParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
