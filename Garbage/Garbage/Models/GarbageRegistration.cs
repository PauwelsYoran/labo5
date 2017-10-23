using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Models
{
    class GarbageRegistration
    {
        public Guid GarbageRegistrationid { get; set; }
        public string Description { get; set; }
        public Guid GarbagetypeId { get; set; }
        public Guid CityId { get; set; }
        public float Weight { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime TimeStamp { get; set; }
    }


}
