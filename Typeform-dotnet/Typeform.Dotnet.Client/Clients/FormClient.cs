using System;
using System.Collections.Generic;
using Typeform.Dotnet.Core;
using Typeform.Dotnet.Data;
using Typeform.Dotnet.Enums;

namespace Typeform.Dotnet.Clients
{

    public class FormClient : Client
    {
        private const string FormResource = "form/{formId}";

        // Filtering Parameter Names
        private const string CompletedFilterName = "completed";
        private const string SinceFilterName = "since";
        private const string UntilFilterName = "until";
        private const string OffsetFilterName = "offset";
        private const string LimitFilterName = "limit";
        private const string OrderByName = "order_by";
        private const string ResponseIdName = "token";

        public string FormId { get; }

        public FormClient(Authentication authentication, string formId) : base(TypeformApiBaseUrl, FormResource, authentication)
        {
            if (string.IsNullOrEmpty(formId))
                throw new ArgumentNullException(nameof(formId));

            if (authentication == null)
                throw new ArgumentNullException(nameof(authentication));

            FormId = formId.Trim();
        }

        public FormClient(string typeformApiUrl, Authentication authentication, string formId)
            : base(
                string.IsNullOrEmpty(typeformApiUrl) ? TypeformApiBaseUrl : typeformApiUrl, FormResource, authentication
                )
        {
            if (string.IsNullOrEmpty(formId))
                throw new ArgumentNullException(nameof(formId));

            if (authentication == null)
                throw new ArgumentNullException(nameof(authentication));

            FormId = formId.Trim();
        }

        public FormResponses GetFormResponses(ResultsCompletedFilterOption completedFilterOption = ResultsCompletedFilterOption.AllResults, DateTimeOffset? resultsSinceDate = null, DateTimeOffset? resultsUntilDate = null, int resultsOffset = 0, int resultsLimit = 0, OrderBy orderBy = OrderBy.Unspecified)
        {
            var parameters = new Dictionary<string, string>();

            // Completed or Incomplete Filter
            switch (completedFilterOption)
            {
                case ResultsCompletedFilterOption.AllResults:
                    break;
                case ResultsCompletedFilterOption.Completed:
                    parameters.Add(CompletedFilterName, "true");
                    break;
                case ResultsCompletedFilterOption.Incomplete:
                    parameters.Add(CompletedFilterName, "false");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(completedFilterOption), completedFilterOption, null);
            }

            // Date Filters
            if (resultsSinceDate != null)
                parameters.Add(SinceFilterName, resultsSinceDate.Value.ToUnixTimeSeconds().ToString());

            if (resultsUntilDate != null)
                parameters.Add(UntilFilterName, resultsUntilDate.Value.ToUnixTimeSeconds().ToString());

            // Paging Options
            if (resultsOffset > 0)
                parameters.Add(OffsetFilterName, resultsOffset.ToString());

            if (resultsLimit > 0)
                parameters.Add(LimitFilterName, resultsLimit.ToString());

            // Order By Specifications
            // TODO: be able to specify more than one ordering option (see: https://www.typeform.com/help/data-api/)
            switch (orderBy)
            {
                case OrderBy.Unspecified:
                    break;
                case OrderBy.Completed:
                    parameters.Add(OrderByName, "completed");
                    break;
                case OrderBy.FormStartedDateAscending:
                    parameters.Add(OrderByName, "date_land");
                    break;
                case OrderBy.FormSubmittedDateAscending:
                    parameters.Add(OrderByName, "date_submit");
                    break;
                case OrderBy.FormStartedDateDescending:
                    parameters.Add(OrderByName, "date_land,desc");
                    break;
                case OrderBy.FormSubmittedDateDescending:
                    parameters.Add(OrderByName, "date_submit,desc");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderBy), orderBy, null);
            }

            var result = Get<FormResponses>(null, parameters, formId: FormId);

            return result.Result;
        }

        // TODO: Get Form Structure (Only Questions)
        // TODO: Get Form Stats Only

        /// <summary>
        /// Get the a specific response to a Typeform
        /// </summary>
        /// <param name="responseId">The ID of the response or the "token"</param>
        public FormResponses GetSpecificFormResponse(string responseId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string> { { ResponseIdName, responseId } };

            var result = Get<FormResponses>(null, parameters, formId: FormId);
            return result.Result;
        }
    }
}