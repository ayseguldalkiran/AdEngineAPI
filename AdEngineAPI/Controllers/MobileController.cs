using AdEngineAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngineAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        [HttpGet("{applicationName}")]
        public IEnumerable<AdModel> getAdvertiseListByName(string applicationName)
        {
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaigns");
            var advertises = collection.Find(g => g.applicationName == applicationName).ToList();
            return advertises;
        }

        [HttpPost("{id}")]
        public string increaseClickCount(int id)
        {
            var clients = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var databases = clients.GetDatabase("AdEngineDB");
            var collections = databases.GetCollection<AdModel>("campaigns");
            var results = collections.Find(x => x.id == id).Limit(1).ToList();
            AdModel oldData = results.FirstOrDefault();
            var oldCount = oldData.clickCount;
           
            var filter = Builders<AdModel>.Filter.Eq("id", id);
            var updateClickCount = Builders<AdModel>.Update.Set("clickCount", oldCount+1);
            collections.UpdateOneAsync(filter, updateClickCount);

            var dayToday = System.DateTime.Now.DayOfWeek.ToString();
            var filterStatistics = Builders<StatisticsModel>.Filter.Eq("dayName", dayToday);
            var collectionStatistics = databases.GetCollection<StatisticsModel>("statistics");
            var resultListStatistics = collectionStatistics.Find(x => x.dayName == dayToday).Limit(1).ToList();
            StatisticsModel singleResultStatistics = resultListStatistics.FirstOrDefault();
            var updateDailyClickCount = Builders<StatisticsModel>.Update.Set("clickCount", singleResultStatistics.clickCount + 1);

            collectionStatistics.UpdateOneAsync(filterStatistics, updateDailyClickCount);

            return (oldCount + 1).ToString() ;
        }
    }
}