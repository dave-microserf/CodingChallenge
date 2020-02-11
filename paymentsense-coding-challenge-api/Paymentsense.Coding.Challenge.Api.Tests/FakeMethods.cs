using System;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Tests
{
    public static class FakeMethods
    {
        public static Task<string> GetCountriesDownloadStringTaskAsync(Uri uri)
        {
            if (uri.ToString() == "https://restcountries.eu/rest/v2/all?fields=name;flag")
            {
                return Task.FromResult(Encoding.UTF8.GetString(Resource.Countries));
            }

            return Task.FromResult(string.Empty);
        }

        public static Task<string> GetCountryInfoDownloadStringTaskAsync(Uri uri)
        {
            if (uri.ToString() == "https://restcountries.eu/rest/v2/name/France")
            {
                return Task.FromResult(Encoding.UTF8.GetString(Resource.France));
            }

            return Task.FromResult(string.Empty);
        }
    }
}