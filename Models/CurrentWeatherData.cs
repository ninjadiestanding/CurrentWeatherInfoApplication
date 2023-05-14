using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentWeatherInfoApplication.Models
{
    internal class CurrentWeatherData
    {
        [JsonProperty("list")]
        public List<City> Cities { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
