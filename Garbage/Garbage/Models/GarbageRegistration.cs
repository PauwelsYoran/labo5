using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Garbage.Models
{
    class GarbageRegistration
    {
        [JsonProperty("garabageregistrationid")]
        public Guid GarbageRegistrationid { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("garbagetypeid")]
        public Guid GarbagetypeId{ get; set; }
        [JsonProperty("cityid")]
        public Guid CityId { get; set; }
        [JsonProperty("weight")]
        public int Weight { get; set; }
        [JsonProperty("lat")]
        public float Lat { get; set; }
        [JsonProperty("long")]
        public float Long { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }
    }


}
