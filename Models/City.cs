using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentWeatherInfoApplication.Models
{
    class City
    {
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("sys")]
        public Country Country { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        public string TemperatureUnit { get; set; }
        public string UnitOfSpeed { get; set; }
        public string TemperatureDisplay => string.Format("{0}{1}", Main.Temperature, TemperatureUnit);
        public string WindSpeedDisplay => string.Format("{0} {1}", Wind.Speed, UnitOfSpeed);
    }
}
