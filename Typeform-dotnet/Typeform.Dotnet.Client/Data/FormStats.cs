using Newtonsoft.Json;

namespace Typeform.Dotnet.Data
{
    public class FormStats
    {
        [JsonProperty("responses")]
        public ResponsesStats ResponseStats { get; set; }
    }
}
