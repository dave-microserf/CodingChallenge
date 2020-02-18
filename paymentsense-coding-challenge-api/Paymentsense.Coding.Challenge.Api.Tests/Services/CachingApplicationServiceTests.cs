using Microsoft.Extensions.Caching.Memory;
using Moq;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CachingApplicationServiceTests
    {
        [Fact]
        public async void GetCountriesAsync_ReturnsExpectedCountriesWhenNotInCache()
        {
            // Arrange
            var applicationService = new Mock<IApplicationService>();

            applicationService.Setup(x => x.GetCountriesAsync()).Returns(
                Task.FromResult(new List<Country> { 
                    new Country { Flag = "https://restcountries.eu/data/afg.svg", Name = "Afghanistan"},
                    new Country { Flag = "https://restcountries.eu/data/ala.svg", Name = "Åland Islands"},
                    new Country { Flag = "https://restcountries.eu/data/alb.svg", Name = "Albania"},
                }));

            var memoryCache = new Mock<IMemoryCache>();
            
            object value = null;
            var cacheEntry = Mock.Of<ICacheEntry>();

            memoryCache.Setup(x => x.TryGetValue(It.Is<string>(cacheKey => cacheKey == "Countries"), out value)).Returns(false);
            memoryCache.Setup(x => x.CreateEntry(It.Is<string>(cacheKey => cacheKey == "Countries"))).Returns(cacheEntry);

            var cachingService = new CachingApplicationService(memoryCache.Object, applicationService.Object);

            // Act
            var countries = await cachingService.GetCountriesAsync();

            // Assert
            Assert.Equal(3, countries.Count);

            // TODO: Improve by mocking memory cache and asserting it gets called
        }
    }
}
