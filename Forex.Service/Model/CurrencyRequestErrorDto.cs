using Newtonsoft.Json;

namespace Forex.Service.Model
{
    // JSON: {"status":400,"error":"Your Free API key has expired. Please check your email (including the Spam folder) to renew your API."}
    public class QuoteRequestErrorDto
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}