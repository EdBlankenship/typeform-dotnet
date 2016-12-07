using System;

namespace Typeform.Dotnet.Core
{
    public class Authentication
    {
        public string ApiKey { private set; get; }

        public Authentication(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            ApiKey = apiKey;
        }
    }
}
