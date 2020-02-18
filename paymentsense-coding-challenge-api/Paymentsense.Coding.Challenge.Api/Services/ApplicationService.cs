using Paymentsense.Coding.Challenge.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class ApplicationService : IApplicationService
    {
        private static readonly Uri RestCountriesAddress = new Uri("https://restcountries.eu/rest/v2/all?fields=name;flag");
        private static readonly Uri RestCountryInfoAddress = new Uri("https://restcountries.eu/rest/v2/name/");

        private readonly IWebClient webClient;

        public ApplicationService(IWebClient webClient)
        {
            if (webClient == null)
            {
                throw new ArgumentNullException(nameof(webClient));
            }
            
            this.webClient = webClient;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            var json = await this.webClient.DownloadStringTaskAsync(RestCountriesAddress);
            return await JsonSerializer.DeserializeAsync<List<Country>>(new MemoryStream(Encoding.UTF8.GetBytes(json)));
        }

        public async Task<CountryInfo> GetCountryInfoAsync(string country)
        {
            var address = new Uri(RestCountryInfoAddress, country);

            var json = await this.webClient.DownloadStringTaskAsync(address);
            var document = await JsonDocument.ParseAsync(new MemoryStream(Encoding.UTF8.GetBytes(json)));
            
            // TODO: refactor out to factory?
            var arrayEnumerator = document.RootElement.EnumerateArray();

            if (arrayEnumerator.MoveNext())
            {
                var countryInfo = new CountryInfo();
                var current = arrayEnumerator.Current;

                countryInfo.Name = current.GetProperty("name").GetString();
                countryInfo.Capital = current.GetProperty("capital").GetString();
                countryInfo.Population = current.GetProperty("population").GetInt32();
                
                var timezones = current.GetProperty("timezones");
                AddProperties(countryInfo.Timezones, timezones);

                var borders = current.GetProperty("borders");
                AddProperties(countryInfo.Borders, borders);

                var currencies = current.GetProperty("currencies").EnumerateArray();
                AddProperties(countryInfo.Currencies, currencies, "name");

                var languages = current.GetProperty("languages").EnumerateArray();
                AddProperties(countryInfo.Languages, languages, "name");

                return countryInfo;
            }
            
            return null;
        }

        private static void AddProperties(ICollection<string> collection, JsonElement array)
        {
            for (int i = 0; i < array.GetArrayLength(); i++)
            {
                collection.Add(array[i].GetString());
            }
        }

        private static void AddProperties(ICollection<string> collection, JsonElement.ArrayEnumerator arrayEnumerator, string propertyName)
        {
            while (arrayEnumerator.MoveNext())
            {
                collection.Add(arrayEnumerator.Current.GetProperty(propertyName).GetString());
            }
        }
    }
}