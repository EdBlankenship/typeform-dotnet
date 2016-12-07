using System;

namespace Typeform.Dotnet.Exceptions
{
    public class JsonConverterException : TypeformException
    {
        public string Json { get; set; }
        public string SerializationType { get; set; }

        public JsonConverterException(string message) : base(message)
        {
            
        }

        public JsonConverterException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
