using Microsoft.Extensions.Caching.Memory;
using Paymentsense.Coding.Challenge.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CachingApplicationService : IApplicationService
    {
        private static readonly string CacheKey = "Countries";

        private readonly IMemoryCache cache;
        private readonly IApplicationService applicationService;

        public CachingApplicationService(IMemoryCache cache, IApplicationService applicationService)
        {
            this.cache = cache;
            this.applicationService = applicationService;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            if (this.cache.TryGetValue(CacheKey, out List<Country> value))
            {
                return value;
            }

            value = await this.applicationService.GetCountriesAsync();
            this.cache.Set(CacheKey, value);
            return value;
        }

        public async Task<CountryInfo> GetCountryInfoAsync(string country)
        {
            return await this.applicationService.GetCountryInfoAsync(country);
        }
    }
}
