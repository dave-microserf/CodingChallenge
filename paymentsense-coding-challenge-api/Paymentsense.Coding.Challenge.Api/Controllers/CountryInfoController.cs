using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryInfoController : ControllerBase
    {
        private readonly ILogger<CountryInfoController> logger;
        private readonly IApplicationService service;
        
        public CountryInfoController(
            ILogger<CountryInfoController> logger,
            IApplicationService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // GET: api/CountryInfo/France
        [HttpGet("{country}", Name = "Get")]
        public async Task<IActionResult> Get(string country)
        {
            try
            {
                return this.Ok(await this.service.GetCountryInfoAsync(country));
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, Constants.ErrorMessage);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}