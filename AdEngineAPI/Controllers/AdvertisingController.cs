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
           

            List <AdModel> exampleAdModels = new List<AdModel>();

            var client = new MongoClient("mongodb+srv://aysegul:cingozrecai@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaigns");
            var document = collection.Find(g => g.companyId>1).ToList();

            return document;


        }

        [HttpGet("{id}")]
        public AdModel Get(int id)
        {
        
           
            return new AdModel();
            
           
        }


        [HttpPost]
        public void NewAdvertise(AdModelClient newAdvertise)
        {
            AdModel AdModelYeni = new AdModel()
            {
                _Id = ObjectId.GenerateNewId(),
                campaignName = newAdvertise.campaignName,
                startTime = newAdvertise.startTime,
                endTime = newAdvertise.endTime,
                id = newAdvertise.id,
                applicationName = newAdvertise.applicationName,
                styleType = newAdvertise.styleType,
                imgUrl = newAdvertise.imgUrl,
                redirectUrl = newAdvertise.redirectUrl,
                duration = newAdvertise.duration,
                companyId = newAdvertise.companyId,

            };
            
            var client = new MongoClient("mongodb+srv://aysegul:cingozrecai@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<AdModel>("campaigns");

 
            collection.InsertOne(AdModelYeni);
      
        }

    }

}
