using Microsoft.Extensions.Caching.Memory;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CachingApplicationServiceTests
    {
        [Fact]
        public async void GetCountriesAsync_ReturnsExpectedCountries()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new ApplicationService(FakeMethods.GetCountriesDownloadStringTaskAsync);
            var cachingService = new CachingApplicationService(memoryCache, service);

            // Act
            var countries = await cachingService.GetCountriesAsync();

            // Assert
            Assert.Equal(3, countries.Count);

            // TODO: Improve by mocking memory cache and asserting it gets called
        }
    }
}
