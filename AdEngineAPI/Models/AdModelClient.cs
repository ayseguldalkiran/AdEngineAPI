using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngineAPI.Models
{
    public class AdModelClient
    {
        public string campaignName { get; set; }
        public int id { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string applicationName { get; set; }
        public string styleType { get; set; }
        public string imgUrl { get; set; }
        public string redirectUrl { get; set; }
        public int duration { get; set; }
        public int companyId { get; set; }

    }
}
