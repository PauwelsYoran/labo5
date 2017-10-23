using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Garbage.Models
{
    class City
    {
        [JsonProperty("cityid")]
        public Guid CityId { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }
    }
}
