using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngineAPI.Models
{
    public class UserModel
    {
        [BsonId]
        public ObjectId _Id { get; set; }
        public string email { get; set; }

        public string password { get; set; }

        public int companyId{ get; set; }

        public string userType { get; set; }

    }
}
