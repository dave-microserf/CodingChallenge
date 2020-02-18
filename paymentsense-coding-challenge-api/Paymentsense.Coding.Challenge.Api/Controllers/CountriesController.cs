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
    public class CountriesController : ControllerBase
    {        
        private readonly ILogger<CountriesController> logger;
        private readonly IApplicationService service;

        public CountriesController(
            ILogger<CountriesController> logger,
            IApplicationService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return this.Ok(await this.service.GetCountriesAsync());
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, Constants.ErrorMessage);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}