using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountryInfoControllerTests
    {
        [Fact]
        public void Get_OnInvoke_ReturnsExpectedCountryInfo()
        {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = factory.CreateLogger<CountryInfoController>();

            var applicationService = new Mock<IApplicationService>();

            applicationService.Setup(x => x.GetCountryInfoAsync(
                It.Is<string>(s => string.Equals(s, "country"))))
                .Returns(Task.FromResult(new CountryInfo { }));

            var controller = new CountryInfoController(logger, applicationService.Object);

            var result = controller.Get("country").Result as OkObjectResult;

            result.Value.Should().NotBe(null);
            result.Value.Should().BeOfType<CountryInfo>();

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}