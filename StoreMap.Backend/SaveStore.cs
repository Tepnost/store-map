using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using StoreMap.Backend.Data.Entities;

namespace StoreMap.Backend
{
    public static class SaveStore
    {
        [FunctionName("SaveStore")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var client = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDBConnectionString"));
            var database = client.GetDatabase("store_map");
            var stores = await database.GetCollection<Store>("stores").FindAsync(x => true);
            
            string name = req.Query["name"];
            stores.ToList().ForEach(x => name = x.Name);


            // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // name ??= data?.name;

            var responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
