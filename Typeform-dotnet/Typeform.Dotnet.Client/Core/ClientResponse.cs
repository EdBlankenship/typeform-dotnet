using RestSharp;
using Typeform.Dotnet.Data;

namespace Typeform.Dotnet.Core
{
    public class ClientResponse<T> where T : class
    {
        public T Result { private set; get; }
        public Errors Errors { private set; get; }
        public IRestResponse Response { private set; get; }

        public ClientResponse(IRestResponse response, T result = null, Errors errors = null)
        {
            Response = response;
            Result = result;
            Errors = errors;
        }
    }
}
