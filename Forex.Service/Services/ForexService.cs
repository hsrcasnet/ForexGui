using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Forex.Service.Model;
using Forex.Service.Services.Exceptions;
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

        public async Task<IEnumerable<QuoteDto>> GetQuotesAsync(string[] pairs)
        {
            // Send API request
            var apiKey = this.forexServiceConfiguration.ApiKey;
            var pairsString = string.Join(",", pairs);
            var uri = $"https://free.currconv.com/api/v7/convert?q={pairsString}&apiKey={apiKey}";
            var httpResponseMessage = await this.httpClient.GetAsync(uri);


            var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                // In case of an error currconv.com returns an error.
                // We deserialize this error and throw it as an exception.
                var quoteRequestErrorDto = JsonConvert.DeserializeObject<QuoteRequestErrorDto>(jsonResponse);
                throw new QuoteRequestException(quoteRequestErrorDto.Status, quoteRequestErrorDto.Error);
            }

            // In case of a successful response,
            // we deserialize the json content to a list of QuoteDtos.
            var rootObject = JsonConvert.DeserializeObject<ConvertResponseDto>(jsonResponse);
            var quoteDtos = rootObject.Results.CurrencyList.Select(c =>
            {
                var quoteDto = c.Value.ToObject<QuoteDto>();
                return quoteDto;
            }).ToList();
            return quoteDtos;
        }
    }
}