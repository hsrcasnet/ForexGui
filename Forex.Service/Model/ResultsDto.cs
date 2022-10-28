using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Forex.Service.Model
{
    internal class ResultsDto
    {
        public ResultsDto()
        {
            this.CurrencyList = new Dictionary<string, JToken>();
        }

        [JsonExtensionData]
        public IDictionary<string, JToken> CurrencyList { get; set; }
    }
}