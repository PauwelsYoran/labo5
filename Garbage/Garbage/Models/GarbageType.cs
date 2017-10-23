using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Garbage.Models
{
    class GarbageType
    {
        [JsonProperty("garbagetypeid")]
        public Guid GarbageTypeId { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }
    }
}
