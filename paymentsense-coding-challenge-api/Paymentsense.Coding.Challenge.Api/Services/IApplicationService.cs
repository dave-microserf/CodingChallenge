using Paymentsense.Coding.Challenge.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface IApplicationService
    {
        Task<List<Country>> GetCountriesAsync();

        Task<CountryInfo> GetCountryInfoAsync(string country);
    }
}