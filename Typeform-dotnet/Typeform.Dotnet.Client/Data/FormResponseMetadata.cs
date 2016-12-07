using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Typeform.Dotnet.Data
{
    public class FormResponseMetadata
    {
        [JsonProperty("browser")]
        public string browser { get; set; }
        [JsonProperty("platform")]
        public string platform { get; set; }
        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>The JSON property returned from Typeform.com is misspelled but the property name in this SDK is spelled correctly.</remarks>
        [JsonProperty("referer")]
        public string Referrer { get; set; }
        [JsonProperty("network_id")]
        public string TypeformUniqueUserId { get; set; }

        // TODO: Revisit this implementation for Submitted Date.

        // Problem:  date_submit is filled in with "0000-00-00 00:00:00" whenever a form has not been submitted.
        // This causes a serialization exception whenever Json.NET attempts to deserialize it into DateTimeOffset.
        // Therefore we're going to ignore it and add it to the Json Extension Data.

        // **** OLD IMPLEMENTATION ****
        //public DateTime? date_submit;
        //[JsonProperty("date_land")]
        //public DateTime FormStartedDate;

        public DateTimeOffset? SubmittedDate { get; set; }
        public DateTimeOffset FormStartedDate { get; set; }

        [JsonExtensionData]
        private IDictionary<string, JToken> _additionalData;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            // date_submit is not deserialized to any property and so it is added to the extension data dictionary
            var dateSubmit = (string)_additionalData["date_submit"];
            if (dateSubmit == "0000-00-00 00:00:00")
            {
                SubmittedDate = null;
            }
            else
            {
                SubmittedDate = (DateTimeOffset)_additionalData["date_submit"];
            }

            // Also go to apply the same logic to date_land so that we can return.  It was serializing as a DateTime correctly but not a DateTimeOffset correctly (because it was converting from UTC returned to local time when JsonSerialization settings were set to DateTimeOffset)
            FormStartedDate = (DateTimeOffset) _additionalData["date_land"];
        }

        public FormResponseMetadata()
        {
            _additionalData = new Dictionary<string, JToken>();
        }
    }


}
