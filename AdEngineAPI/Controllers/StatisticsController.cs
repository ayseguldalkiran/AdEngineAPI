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
    public class StatisticsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<StatisticsModel> getAllStatistics()
        {

            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<StatisticsModel>("statistics");
            var document = collection.Find(g => g.clickCount >= 1).ToList();
            

            return document;
        }

        /*
        [HttpGet("{day}")]
        public StatisticsModel getStatisticsByDay(string day)
        {
            //var dayToday = System.DateTime.Now.DayOfWeek.ToString();

            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<StatisticsModel>("statistics");
            var results = collection.Find(x => x.dayName == day ).Limit(1).ToList();
            StatisticsModel singleResult = results.FirstOrDefault();

            return singleResult;
        }*/
    }
}