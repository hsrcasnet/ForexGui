using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Forex.Service.Model;
using Newtonsoft.Json;

namespace Forex.Service.Services
{
    public class ForexService : IForexService
    {
        private readonly IForexServiceConfiguration forexServiceConfiguration;
        private readonly HttpClient httpClient;

        public ForexService(IForexServiceConfiguration forexServiceConfiguration)
        {
            this.forexServiceConfiguration = forexServiceConfiguration;
            this.httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Quote>> GetQuotes(string[] pairs)
        {
            var apiKey = this.forexServiceConfiguration.ApiKey;
            var pairsString = string.Join(",", pairs);
            var uri = $"https://forex.1forge.com/1.0.3/quotes?pairs={pairsString}&api_key={apiKey}";
            var httpResponseMessage = await this.httpClient.GetAsync(uri);
            httpResponseMessage.EnsureSuccessStatusCode();

            var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var quotes = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Quote>>(jsonResponse));
            return quotes;
        }
    }
}