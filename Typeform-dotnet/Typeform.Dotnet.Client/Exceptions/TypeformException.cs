using System;

namespace Typeform.Dotnet.Exceptions
{
    public class TypeformException : Exception
    {
        public TypeformException()
        {

        }

        public TypeformException(string message) : base(message)
        {
            
        }

        public TypeformException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
}
}
