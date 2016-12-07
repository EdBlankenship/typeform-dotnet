using RestSharp;
using RestSharp.Authenticators;

namespace Typeform.Dotnet.Core
{
    public class ApiKeyAuthenticator : IAuthenticator
    {
        private readonly string _apiKeyName;
        private readonly string _apiKeyValue;

        public ApiKeyAuthenticator(string keyName, string keyValue)
        {
            _apiKeyName = keyName;
            _apiKeyValue = keyValue;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddParameter(_apiKeyName, _apiKeyValue);
        }
    }
}
