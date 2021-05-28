using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngineAPI.Models
{
    public class StatisticsModel
    {
        [BsonId]

        public ObjectId _Id { get; set; }
        public string dayName { get; set; }
        public int clickCount { get; set; }
        public int dailyCost { get; set; }
    }
}
