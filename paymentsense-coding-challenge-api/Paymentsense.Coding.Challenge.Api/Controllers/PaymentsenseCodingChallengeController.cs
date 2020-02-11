using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paymentsense.Coding.Challenge.Api.Services;
using System;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsenseCodingChallengeController : ControllerBase
    {
        private static readonly string ErrorMessage = "An unhandled exception has occurred.";

        private readonly ILogger<PaymentsenseCodingChallengeController> logger;
        private readonly IApplicationService service;

        public PaymentsenseCodingChallengeController(
            ILogger<PaymentsenseCodingChallengeController> logger, 
            IApplicationService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Paymentsense Coding Challenge!");
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountriesAsync()
        {
            try
            {
                return this.Ok(await this.service.GetCountriesAsync());
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, ErrorMessage);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("countryInfo/{country}")]
        public async Task<IActionResult> GetCountryInfoAsync(string country)
        {
            try
            {
                return this.Ok(await this.service.GetCountryInfoAsync(country));
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, ErrorMessage);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}