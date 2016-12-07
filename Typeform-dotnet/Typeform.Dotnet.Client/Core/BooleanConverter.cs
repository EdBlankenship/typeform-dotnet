using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Typeform.Dotnet.Exceptions;

namespace Typeform.Dotnet.Core
{
    public class BooleanConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                // TODO:  Check whether the output type is nullable and if not, throw JsonSerializationException
                return null;
            }

            try
            {
                switch (reader.TokenType)
                {
                    case JsonToken.String:
                        string booleanText = reader.Value.ToString().Trim();

                        // First try to see if the string is "0" or "1"
                        switch (booleanText)
                        {
                            case "0":
                                return false;
                            case "1":
                                return true;
                        }

                        // Fallback to converting from "true" or "false"
                        return bool.Parse(booleanText);
                    case JsonToken.Boolean:
                        return reader.Value;
                    case JsonToken.Integer:
                        var booleanInt = (int) reader.Value;

                        if (booleanInt > 0)
                            return true;
                        if (booleanInt <= 0)
                            return false;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new JsonConverterException($"Error converting value {reader.Value} to type {objectType}", ex);
            }

            // We shouldn't get here.
            throw new JsonConverterException($"Unexpected token {reader.TokenType}, when parsing boolean.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}
