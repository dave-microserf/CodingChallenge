using Moq;
using Paymentsense.Coding.Challenge.Api.Services;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class ApplicationServiceTests
    {
        [Fact]
        public async void GetCountriesAsync_ReturnsExpectedCountries()
        {
            // Arrange
            var mock = new Mock<IWebClient>();
            
            mock.Setup(x => x.DownloadStringTaskAsync(
                It.Is<Uri>(uri => uri.ToString() == "https://restcountries.eu/rest/v2/all?fields=name;flag")))
                .Returns(Task.FromResult(Encoding.UTF8.GetString(Resource.Countries)));

            var service = new ApplicationService(mock.Object);

            // Act
            var countries = await service.GetCountriesAsync();

            // Assert
            Assert.Collection(countries,
                country =>
                {
                    Assert.Equal("Afghanistan", country.Name);
                    Assert.Equal("https://restcountries.eu/data/afg.svg", country.Flag);
                },
                country =>
                {
                    Assert.Equal("Åland Islands", country.Name);
                    Assert.Equal("https://restcountries.eu/data/ala.svg", country.Flag);
                },
                country =>
                {
                    Assert.Equal("Albania", country.Name);
                    Assert.Equal("https://restcountries.eu/data/alb.svg", country.Flag);
                });
        }

        [Fact]
        public async void GetCountryInfoAsync_ReturnsExpectedCountryInfo()
        {
            // Arrange
            var mock = new Mock<IWebClient>();

            mock.Setup(x => x.DownloadStringTaskAsync(
                It.Is<Uri>(uri => uri.ToString() == "https://restcountries.eu/rest/v2/name/France")))
                .Returns(Task.FromResult(Encoding.UTF8.GetString(Resource.France)));

            var service = new ApplicationService(mock.Object);

            // Act
            var countryInfo = await service.GetCountryInfoAsync("France");

            // Assert
            Assert.Equal("France", countryInfo.Name);
            Assert.Equal("Paris", countryInfo.Capital);
            Assert.Equal(66710000, countryInfo.Population);
            
            Assert.Collection(countryInfo.Currencies, ccy => Assert.Equal("Euro", ccy));
            Assert.Collection(countryInfo.Languages, lang => Assert.Equal("French", lang));
           
            Assert.Collection(countryInfo.Timezones,
                tz => Assert.Equal("UTC-10:00", tz),
                tz => Assert.Equal("UTC-09:30", tz),
                tz => Assert.Equal("UTC-09:00", tz),
                tz => Assert.Equal("UTC-08:00", tz),
                tz => Assert.Equal("UTC-04:00", tz),
                tz => Assert.Equal("UTC-03:00", tz),
                tz => Assert.Equal("UTC+01:00", tz),
                tz => Assert.Equal("UTC+03:00", tz),
                tz => Assert.Equal("UTC+04:00", tz),
                tz => Assert.Equal("UTC+05:00", tz),
                tz => Assert.Equal("UTC+11:00", tz),
                tz => Assert.Equal("UTC+12:00", tz));
                       
            Assert.Collection(countryInfo.Borders, 
                border => Assert.Equal("AND", border),
                border => Assert.Equal("BEL", border),
                border => Assert.Equal("DEU", border),
                border => Assert.Equal("ITA", border),
                border => Assert.Equal("LUX", border),
                border => Assert.Equal("MCO", border),
                border => Assert.Equal("ESP", border),
                border => Assert.Equal("CHE", border));
        }
    }
}