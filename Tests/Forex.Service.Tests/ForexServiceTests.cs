using System.Threading.Tasks;
using FluentAssertions;
using Forex.Service.Services;
using Xunit;

namespace Forex.Service.Tests
{
    public class ForexServiceTests
    {
        [Fact]
        [Trait("Category", "SystemTest")]
        public async Task ShouldGetQuotes_WithCurrencyPairs_Empty()
        {
            // Arrange
            var pairs = new string[] { };
            var forexService = new ForexService(new ForexServiceConfiguration());

            // Act
            var quotes = await forexService.GetQuotes(pairs);

            // Assert
            quotes.Should().NotBeNullOrEmpty();
        }

        [Fact]
        [Trait("Category", "SystemTest")]
        public async Task ShouldGetQuotes_WithCurrencyPairs_CHFUSD()
        {
            // Arrange
            var pairs = new[] { "CHFUSD" };
            var forexService = new ForexService(new ForexServiceConfiguration());

            // Act
            var quotes = await forexService.GetQuotes(pairs);

            // Assert
            quotes.Should().NotBeNullOrEmpty();
        }
    }
}