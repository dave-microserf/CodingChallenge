using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class CountryInfo
    {
        public CountryInfo()
        {
            this.Currencies = new List<string>();
            this.Languages = new List<string>();
            this.Timezones = new List<string>();
            this.Borders = new List<string>();
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get; set;
        }

        [JsonPropertyName("capital")]
        public string Capital
        {
            get; set;
        }

        [JsonPropertyName("population")]
        public int Population
        {
            get; set;
        }

        [JsonPropertyName("currencies")]
        public ICollection<string> Currencies
        {
            get;
        }

        [JsonPropertyName("languages")]
        public ICollection<string> Languages
        {
            get;
        }

        [JsonPropertyName("timezones")]
        public ICollection<string> Timezones
        {
            get;
        }

        [JsonPropertyName("borders")]
        public ICollection<string> Borders
        {
            get;
        }
    }
}