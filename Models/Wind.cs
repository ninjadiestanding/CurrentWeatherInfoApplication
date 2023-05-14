using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentWeatherInfoApplication.Models
{
    internal class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}
