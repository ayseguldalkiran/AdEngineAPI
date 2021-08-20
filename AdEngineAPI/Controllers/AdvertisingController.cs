using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdEngineAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using AdEngineAPI.Helpers;
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
            var document = collection.Find(advertise => advertise.id >= 1).ToList();
            return document;

        } 
        [HttpGet("/GetAllAdvertiseRequests")]
        public IEnumerable<AdModel> GetAllAdvertiseRequests()
        {
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaignRequests");
            var document = collection.Find(advertise => advertise.id >= 1).ToList();
            return document;

        }
       

        [HttpGet("/GetAdvertiseRequest/{id}")]
        public AdModel GetAdvertiseRequest(int id)
        {
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaignRequests");
            var results = collection.Find(x => x.id == id).Limit(1).ToList();
            AdModel singleResult = results.FirstOrDefault();

            return singleResult;

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
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaigns");
         var collectionRequests = database.GetCollection<AdModel>("campaignRequests");
            AdModel resultCampaign = collectionRequests.Find(x => true).SortByDescending(a => a.id).ToList().FirstOrDefault();
            AdModel resultPending = collection.Find(x => true).SortByDescending(a => a.id).ToList().FirstOrDefault();
            AdModel AdModelYeni = new AdModel()
            {
                _Id = ObjectId.GenerateNewId(),
                campaignName = newAdvertise.campaignName,
                startTime = newAdvertise.startTime,
                endTime = newAdvertise.endTime,
                id = (resultPending == null && resultCampaign == null) ? 1 : (resultCampaign.id > resultPending.id ? resultCampaign.id + 1 : resultPending.id + 1),
                styleType = newAdvertise.styleType,
                imgUrl = newAdvertise.imgUrl,
                applicationName = newAdvertise.applicationName,
                redirectUrl = newAdvertise.redirectUrl,
                duration = newAdvertise.duration,
                companyId = newAdvertise.companyId,
                clickCount = 0
            };

            


            collection.InsertOne(AdModelYeni);

            return newAdvertise;

        }

        [HttpPost ("/AddAdvertiseRequest")]
        public AdModelClient NewAdvertiseRequest(AdModelClient newAdvertise)
        {
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaignRequests");
            var collectionCampaigns = database.GetCollection<AdModel>("campaigns");
            AdModel resultCampaign = collectionCampaigns.Find(x => true).SortByDescending(a => a.id).ToList().FirstOrDefault();
            AdModel resultPending = collection.Find(x => true).SortByDescending(a => a.id).ToList().FirstOrDefault();
            
            AdModel AdModelYeni = new AdModel()
            {
                _Id = ObjectId.GenerateNewId(),
                campaignName = newAdvertise.campaignName,
                startTime = newAdvertise.startTime,
                endTime = newAdvertise.endTime,
                id = (resultPending==null && resultCampaign==null)  ? 1 :(resultCampaign.id > resultPending.id ? resultCampaign.id +1 : resultPending.id +1 ),
                styleType = newAdvertise.styleType,
                imgUrl = newAdvertise.imgUrl,
                applicationName = newAdvertise.applicationName,
                redirectUrl = newAdvertise.redirectUrl,
                duration = newAdvertise.duration,
                companyId = newAdvertise.companyId,
                clickCount = 0
            };
           
            collection.InsertOne(AdModelYeni);
            return newAdvertise;

        }
        [HttpGet("/approveAdvertise/{id}/{option}")]
        public string ApproveRequest(int id,string option)
        {
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var requestCollection = database.GetCollection<AdModel>("campaignRequests");
            var campaignCollection = database.GetCollection<AdModel>("campaigns");
            if (option == "approve")
            {
                var results = requestCollection.Find(x => x.id == id).Limit(1).ToList();
                AdModel singleResult = results.FirstOrDefault();
                campaignCollection.InsertOne(singleResult);
                var filter = Builders<AdModel>.Filter.Eq("id", id);
                requestCollection.DeleteOne(filter);
            }
            else if (option == "decline")
            {
                var filter = Builders<AdModel>.Filter.Eq("id", id);
                requestCollection.DeleteOne(filter);
            }
            return "";
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
        [HttpDelete("/deleteCampaign/{id}")]
        public string deleteCampaign(string id)
        {
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaigns");
            var filter = Builders<AdModel>.Filter.Eq("id", id);
            collection.DeleteOne(filter);
            return "Succesfully Deleted";
        }

    }

}
