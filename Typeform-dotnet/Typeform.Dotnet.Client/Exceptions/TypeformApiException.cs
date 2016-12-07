using System;
using Typeform.Dotnet.Data;

namespace Typeform.Dotnet.Exceptions
{
    public class TypeformApiException : TypeformException
    {
        public int StatusCode { set; get; }
        public string StatusDescription { set; get; }
        public string ApiResponseBody { get; set; }
        public Errors ApiErrors { get; set; }

        public TypeformApiException()
        {
            
        }

        public TypeformApiException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public TypeformApiException(int statusCode, string statusDescription, Errors apiErrors, string apiResponseBody)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            ApiErrors = apiErrors;
            ApiResponseBody = apiResponseBody;
        }

        public TypeformApiException(string message, Exception innerException, int statusCode, string statusDescription,
            Errors apiErrors, string apiResponseBody) : base(message, innerException)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            ApiErrors = apiErrors;
            ApiResponseBody = apiResponseBody;
        }

    }
}
