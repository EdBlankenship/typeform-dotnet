using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using Typeform.Dotnet.Data;
using Typeform.Dotnet.Exceptions;

namespace Typeform.Dotnet.Core
{
    public class Client
    {
        protected const string TypeformApiBaseUrl = "https://api.typeform.com/v1";
        protected const string ApiKeyName = "key";
        protected const string ContentTypeHeader = "Content-Type";
        protected const string ContentTypeValue = "application/json";
        protected const string AcceptHeader = "Accept";
        protected const string AcceptValue = "application/json";
        protected const string AcceptCharsetHeader = "Accept-Charset";
        protected const string AcceptCharsetValue = "UTF-8";
        protected const string UserAgentHeader = "User-Agent";
        protected const string UserAgentValue = "typeform-dotnet/1.0.0";
        protected const string FormIdUrlReplacementParameter = "{formId}";
        protected const string FormIdUrlReplacementName = "formId";
        protected const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        protected readonly string Url;
        protected readonly string Resource;
        protected readonly Authentication Auth;

        public Client(string url, string resource, Authentication authentication)
        {
            if (authentication == null)
                throw new ArgumentNullException(nameof(authentication));

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrEmpty(resource))
                throw new ArgumentNullException(nameof(resource));

            Url = url;
            Resource = resource;
            Auth = authentication;
        }

        protected virtual ClientResponse<T> Get<T>(Dictionary<string, string> headers = null,
            Dictionary<string, string> parameters = null, string resource = null, string formId = null) where T : class
        {
            IRestClient client = BuildClient();
            IRestRequest request = BuildRequest(httpMethod: Method.GET, headers: headers, parameters: parameters,
                resource: resource, formId: formId);
            IRestResponse response = client.Execute(request);

            var clientResponse = HandleResponse<T>(response);

            return clientResponse;
        }

        //protected virtual T GetAlternate<T>(Dictionary<string, string> headers = null,
        //    Dictionary<string, string> parameters = null, string resource = null, string formId = null) where T : new()
        //{
        //    IRestClient client = BuildClient();
        //    IRestRequest request = BuildRequest(httpMethod: Method.GET, headers: headers, parameters: parameters,
        //        resource: resource, formId: formId);
            
        //    var response = client.Execute<T>(request);

        //    return response.Data;
        //}


        protected virtual IRestClient BuildClient()
        {
            RestClient client = new RestClient(Url) { Authenticator = new ApiKeyAuthenticator(ApiKeyName, Auth.ApiKey) };
            
            // TODO - Potentially come back here to clean this up
            //client.ClearHandlers();
            //client.AddHandler("application/json", new TypeformDeserializer());
            //client.AddHandler("text/json", new TypeformDeserializer());
            //client.AddHandler("*+json", new TypeformDeserializer());

            return client;
        }

        protected virtual IRestRequest BuildRequest(Method httpMethod = Method.GET,
            Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null, string body = null,
            string resource = null, string formId = null)
        {
            string finalResource = string.IsNullOrEmpty(resource) ? Resource : resource;

            if(finalResource.Contains(FormIdUrlReplacementParameter) && string.IsNullOrEmpty(formId))
                throw new ArgumentNullException(formId);

            IRestRequest request = new RestRequest(finalResource, httpMethod);
            if (finalResource.Contains(FormIdUrlReplacementParameter))
                request.AddParameter(FormIdUrlReplacementName, formId, ParameterType.UrlSegment);

            request.AddHeader(ContentTypeHeader, ContentTypeValue);
            request.AddHeader(AcceptCharsetHeader, AcceptCharsetValue);
            request.AddHeader(AcceptHeader, AcceptValue);
            request.AddHeader(UserAgentHeader, UserAgentValue);

            if (headers != null && headers.Any())
                AddHeaders(request, headers);

            if (parameters != null && parameters.Any())
                AddParameters(request, parameters);

            if (!string.IsNullOrEmpty(body))
                AddBody(request, body);

            return request;
        }

        protected virtual void AddHeaders(IRestRequest request, Dictionary<string, string> headers)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            foreach (var header in headers)
            {
                request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }
        }

        protected virtual void AddParameters(IRestRequest request, Dictionary<string, string> parameters)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value, ParameterType.QueryString);
            }
        }

        protected virtual void AddBody(IRestRequest request, string body)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (!string.IsNullOrEmpty(body))
                request.AddParameter("application/json", body, ParameterType.RequestBody);
        }

        protected virtual ClientResponse<T> HandleResponse<T>(IRestResponse response) where T : class
        {
            ClientResponse<T> clientResponse;
            int statusCode = (int)response.StatusCode;

            if (statusCode >= 200 && statusCode < 300)
                clientResponse = HandleNormalResponse<T>(response);
            else
            {
                clientResponse = HandleErrorReponse<T>(response);
            }

            return clientResponse;
        }

        protected ClientResponse<T> HandleNormalResponse<T>(IRestResponse response) where T : class
        {
            return new ClientResponse<T>(response, Deserialize<T>(response.Content));
        }

        protected ClientResponse<T> HandleErrorReponse<T>(IRestResponse response) where T : class
        {
            if (string.IsNullOrEmpty(response.Content))
            {
                return new ClientResponse<T>(response);
            }
            else
            {
                Errors errors = Deserialize<Errors>(response.Content);
                return new ClientResponse<T>(response, errors: errors);
            }
        }

        protected T Deserialize<T>(string data) where T : class
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                DateFormatString = DateFormat,
                //DateParseHandling = DateParseHandling.DateTimeOffset,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            return JsonConvert.DeserializeObject<T>(data, settings);
        }

        protected void AssertIfAnyErrors<T>(ClientResponse<T> response) where T : class
        {
            if (response.Errors?.ErrorsList != null && response.Errors.ErrorsList.Any())
            {
                throw new TypeformApiException((int)response.Response.StatusCode,
                    response.Response.StatusDescription,
                    response.Errors,
                    response.Response.Content);
            }
        }
    }
}
