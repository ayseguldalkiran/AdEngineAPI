using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngineAPI.Models
{
        
    public class AdModel
    {
        [BsonId]
        public ObjectId _Id { get; set; }
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
        public int clickCount { get; set; }
    }
}
