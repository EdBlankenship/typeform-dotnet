using System.Collections.Generic;

namespace Typeform.Dotnet.Data
{
    public class Errors
    {
        public string Type { get; set; }
        public string RequestId { get; set; }

        public List<Error> ErrorsList { get; set; }
    }

}
