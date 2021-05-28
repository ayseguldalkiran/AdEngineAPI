using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdEngineAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
// ...


namespace AdEngineAPI.Controllers


{
    [ApiController]
    [Route("[controller]")]

    public class AdvertisingController : Controller
    {
        

        [HttpGet]
        public IEnumerable<AdModel> Get()
        {
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaigns");
            var document = collection.Find(advertise => advertise.id>=1).ToList();

            return document;

        }

        [HttpGet("{id}")]
        public AdModel Get(int id)
        {
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaigns");
            var results = collection.Find(x => x.id == id).Limit(1).ToList();
            AdModel singleResult = results.FirstOrDefault();

            return singleResult;

        }


        [HttpPost]
        public AdModelClient NewAdvertise(AdModelClient newAdvertise)
        {
            AdModel AdModelYeni = new AdModel()
            {
                _Id = ObjectId.GenerateNewId(),
                campaignName = newAdvertise.campaignName,
                startTime = newAdvertise.startTime,
                endTime = newAdvertise.endTime,
                id = newAdvertise.id,
                styleType = newAdvertise.styleType,
                imgUrl = newAdvertise.imgUrl,
                applicationName = newAdvertise.applicationName,
                redirectUrl = newAdvertise.redirectUrl,
                duration = newAdvertise.duration,
                companyId = newAdvertise.companyId,
                clickCount = 0
            };
            
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaigns");

 
            collection.InsertOne(AdModelYeni);

            return newAdvertise;
      
        }

        [HttpPut]
        public AdModelClient UpdateAdvertise(AdModelClient Advertise)
        {
            var clients = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var databases = clients.GetDatabase("AdEngineDB");
            var collections = databases.GetCollection<AdModel>("campaigns");
            var results = collections.Find(x => x.id == Advertise.id).Limit(1).ToList();
            AdModel singleResult = results.FirstOrDefault();
            AdModel AdModelYeni = new AdModel()
            {
                _Id = ObjectId.GenerateNewId(),
                campaignName = Advertise.campaignName,
                startTime = Advertise.startTime,
                endTime = Advertise.endTime,
                id = Advertise.id,
                styleType = Advertise.styleType,
                imgUrl = Advertise.imgUrl,
                applicationName = Advertise.applicationName,
                redirectUrl = Advertise.redirectUrl,
                duration = Advertise.duration,
                companyId = Advertise.companyId,
                clickCount = singleResult.clickCount
               
            };

            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaigns");


            var filter = Builders<AdModel>.Filter.Eq("id", AdModelYeni.id);
            var updateCampaignName = Builders<AdModel>.Update.Set("campaignName", AdModelYeni.campaignName);
            var updateStartTime = Builders<AdModel>.Update.Set("startTime", AdModelYeni.startTime);
            var updateEndTime= Builders<AdModel>.Update.Set("endTime", AdModelYeni.endTime);
            var updateStyleType = Builders<AdModel>.Update.Set("styleType", AdModelYeni.styleType);
            var updateImgUrl = Builders<AdModel>.Update.Set("imgUrl", AdModelYeni.imgUrl);
            var updateApplicationName = Builders<AdModel>.Update.Set("applicationName", AdModelYeni.applicationName);
            var updateRedirectUrl = Builders<AdModel>.Update.Set("redirectUrl", AdModelYeni.redirectUrl);
            var updateDuration = Builders<AdModel>.Update.Set("duration", AdModelYeni.duration);
            var updateCompanyId = Builders<AdModel>.Update.Set("companyId", AdModelYeni.companyId);
            collection.UpdateOne(filter, updateCampaignName);
            collection.UpdateOne(filter, updateStartTime);
            collection.UpdateOne(filter, updateEndTime);
            collection.UpdateOne(filter, updateStyleType);
            collection.UpdateOne(filter, updateImgUrl);
            collection.UpdateOne(filter, updateApplicationName);
            collection.UpdateOne(filter, updateRedirectUrl);
            collection.UpdateOne(filter, updateDuration);
            collection.UpdateOne(filter, updateCompanyId);
            return Advertise;

        }

    }

}
