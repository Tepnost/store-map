using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StoreMap.Backend.Data.Interfaces;

namespace StoreMap.Backend
{
    public class SaveStore
    {
        private readonly IStoreRepository storeRepository;

        public SaveStore(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        [FunctionName("SaveStore")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string name = req.Query["name"];
            var stores = await storeRepository.GetAllStores();
            stores.ForEach(x => name = x.Name);
            
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
