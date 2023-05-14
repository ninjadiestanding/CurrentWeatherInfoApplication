using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentWeatherInfoApplication.Models
{
    class Country
    {
        [JsonProperty("country")]
        public string Code { get; set; }
    }
}
