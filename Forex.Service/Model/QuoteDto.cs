using Newtonsoft.Json;

namespace Forex.Service.Model
{
    //JSON String: "id": "EUR_CHF", "val": 1.08765, "to": "CHF", "fr": "EUR"
    public class QuoteDto
    {
        [JsonProperty("id")]
        public string Symbol { get; set; }

        [JsonProperty("val")]
        public decimal Price { get; set; }
    }
}