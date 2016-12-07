using System;
using Newtonsoft.Json;
using Typeform.Dotnet.Enums;

namespace Typeform.Dotnet.Data
{
    public class Question
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("question")]
        public string Text { get; set; }
        [JsonProperty("field_id")]
        public int FieldId { get; set; }
        [JsonProperty("group")]
        public string Group { get; set; }

        public QuestionType? Type
        {
            get
            {
                if (string.IsNullOrEmpty(Id))
                    return null;

                string fieldType = Id.Split('_')[0];
                switch (fieldType)
                {
                    case "textfield":
                        return QuestionType.ShortText;
                    case "textarea":
                        return QuestionType.LongText;
                    case "statement":
                        return QuestionType.Statement;
                    case "dropdown":
                        return QuestionType.Dropdown;
                    case "email":
                        return QuestionType.Email;
                    case "date":
                        return QuestionType.Date;
                    case "terms":
                        return QuestionType.Legal;
                    case "website":
                        return QuestionType.Website;
                    case "list":
                        return QuestionType.List;
                    case "imagelist":
                        return QuestionType.ImageList;
                    case "group":
                        return QuestionType.QuestionGroup;
                    case "yesno":
                        return QuestionType.YesNo;
                    case "rating":
                        return QuestionType.Rating;
                    case "opinionscale":
                        return QuestionType.OpinionScale;
                    case "number":
                        return QuestionType.Number;
                    case "fileupload":
                        return QuestionType.FileUpload;
                    case "payment":
                        return QuestionType.Payment;
                    default:
                        throw new NotImplementedException($"Unknown Typeform.com field type: {fieldType}");
                }
            }
        }

        // TODO
        public bool QuestionTextIncludesPlaceholder { get; }
    }
}
