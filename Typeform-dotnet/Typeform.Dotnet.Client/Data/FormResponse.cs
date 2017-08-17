using System.Collections.Generic;
using Newtonsoft.Json;
using Typeform.Dotnet.Core;

namespace Typeform.Dotnet.Data
{
    public class FormResponse
    {
        [JsonConverter(typeof(BooleanConverter))]
        [JsonProperty("completed")]
        public bool Completed { get; set; }
        [JsonProperty("token")]
        public string ResponseId { get; set; }
        [JsonProperty("metadata")]
        public FormResponseMetadata Metadata { get; set; }

        // TODO:  Handle collection
        [JsonProperty("hidden")]
        public Dictionary<string, string> Hidden { get; set; }

        // TODO:  Handle collection
        [JsonProperty("answers")]
        public Dictionary<string, string> Answers { get; set; }
    }
}
