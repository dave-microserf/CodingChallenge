using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class PaymentsenseCodingChallengeControllerTests
    {
        [Fact]
        public void Get_OnInvoke_ReturnsExpectedMessage()
        {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = factory.CreateLogger<PaymentsenseCodingChallengeController>();

            var service = new ApplicationService();
            var controller = new PaymentsenseCodingChallengeController(logger, service);

            var result = controller.Get().Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().Be("Paymentsense Coding Challenge!");
        }

        [Fact]
        public void GetCountries_OnInvoke_ReturnsExpectedCountries()
        {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = factory.CreateLogger<PaymentsenseCodingChallengeController>();

            var service = new ApplicationService(FakeMethods.GetCountriesDownloadStringTaskAsync);
            var controller = new PaymentsenseCodingChallengeController(logger, service);

            var result = controller.GetCountriesAsync().Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void GetCountryInfo_OnInvoke_ReturnsExpectedCountryInfo()
        {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = factory.CreateLogger<PaymentsenseCodingChallengeController>();

            var service = new ApplicationService(FakeMethods.GetCountryInfoDownloadStringTaskAsync);
            var controller = new PaymentsenseCodingChallengeController(logger, service);

            var result = controller.GetCountryInfoAsync("France").Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}