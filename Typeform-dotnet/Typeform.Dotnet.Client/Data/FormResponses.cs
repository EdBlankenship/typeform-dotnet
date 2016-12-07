using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Typeform.Dotnet.Data
{
    public class FormResponses
    {
        [JsonProperty("stats")]
        public FormStats Stats { get; set; }

        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }

        public Dictionary<string, Question> QuestionsDictionary
        {
            get
            {
                if (Questions == null)
                    return null;

                if (!Questions.Any())
                    return new Dictionary<string, Question>();

                return Questions.ToDictionary(question => question.Id);
            }
        }

        [JsonProperty("responses")]
        public List<FormResponse> Responses { get; set; }

        [JsonProperty("http_status")]
        public int HttpStatusCode { get; set; }
    }
}
