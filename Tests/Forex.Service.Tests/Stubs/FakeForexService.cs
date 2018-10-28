using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forex.Service.Model;

namespace Forex.Service.Services
{
    public class FakeForexService : IForexService
    {
        private static readonly Random Rng = new Random();

        public async Task<IEnumerable<Quote>> GetQuotes(string[] pairs)
        {
            var quotes = new List<Quote>();
            foreach (var symbol in pairs)
            {
                var dto = new Quote
                {
                    Symbol = symbol,
                    Price = (decimal)Rng.NextDouble() * Rng.Next(1, 100),
                };
                quotes.Add(dto);
            }

            await Task.Delay(1000);

            return quotes;
        }
    }
}