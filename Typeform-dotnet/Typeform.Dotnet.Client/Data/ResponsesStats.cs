using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace Typeform.Dotnet.Data
{
    public class ResponsesStats
    {
        [JsonProperty("showing")]
        public int ResponsesIncludedInCall { get; set; }
        [JsonProperty("total")]
        public int TotalResponses { get; set; }
        [JsonProperty("completed")]
        public int ResponsesCompleted { get; set; }

    }
}
