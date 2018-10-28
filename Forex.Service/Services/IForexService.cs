using System.Collections.Generic;
using System.Threading.Tasks;
using Forex.Service.Model;

namespace Forex.Service.Services
{
    public interface IForexService
    {
        Task<IEnumerable<Quote>> GetQuotes(string[] pairs);
    }
}