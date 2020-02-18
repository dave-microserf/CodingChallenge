using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountriesControllerTests
    {
        [Fact]
        public void Get_OnInvoke_ReturnsExpectedCountries()
        {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = factory.CreateLogger<CountriesController>();

            var applicationService = new Mock<IApplicationService>();

            applicationService.Setup(x => x.GetCountriesAsync()).Returns(
                Task.FromResult(new List<Country> { }));

            var controller = new CountriesController(logger, applicationService.Object);

            var result = controller.Get().Result as OkObjectResult;
            
            result.Value.Should().NotBe(null);
            result.Value.Should().BeOfType<List<Country>>();

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}