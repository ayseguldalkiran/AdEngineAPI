using System;
using System.Collections.Generic;
using System.Linq;
using AdEngineAPI.Helpers;
using AdEngineAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AdEngineAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public string loginUser(UserClientModel userClient)
        {
            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<UserModel>("users");
            var document = collection.Find(user => user.email == userClient.email).Limit(1).ToList();
            UserModel singleResult = document.FirstOrDefault();


            var encryptedString = AesOperation.EncryptString(Secrets.encryptionKey, userClient.password);
     

                if (encryptedString.Equals(singleResult.password))
                {
                    return singleResult.userType;
                }
                return "false";

            
        }

        [HttpPost("/createUser")]
        public string createUser(UserClientModel inputs)
        {

            UserModel UserModelYeni = new UserModel
            {
                _Id = MongoDB.Bson.ObjectId.GenerateNewId(),
                email = inputs.email,
                password = AesOperation.EncryptString(Secrets.encryptionKey, inputs.password),
                companyId = inputs.companyId,
                userType = "customer"
            };

            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<UserModel>("users");
            collection.InsertOne(UserModelYeni);

            return "Kayıt Başarılı";
        } 

        [HttpPost("/createAdmin")]
        public string createAdmin(UserClientModel inputs)
        {

            UserModel UserModelYeni = new UserModel
            {
                _Id = MongoDB.Bson.ObjectId.GenerateNewId(),
                email = inputs.email,
                password = AesOperation.EncryptString(Secrets.encryptionKey, inputs.password),
                companyId = inputs.companyId,
                userType = "admin"
            };

            var client = new MongoClient("mongodb+srv://aysegul:adenginepassword@cluster0.zpn92.mongodb.net/AdEngineDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("AdEngineDB");
            var collection = database.GetCollection<UserModel>("users");
            collection.InsertOne(UserModelYeni);

            return "Kayıt Başarılı";
        }  
        

    }
}