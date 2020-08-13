
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.Commands;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend
{
    public class SaveStore : FunctionBase
    {
        private SaveStoreCommand command;
        
        public SaveStore(IServiceProvider serviceProvider, SaveStoreCommand command) : base(serviceProvider)
        {
            this.command = command;
        }
        
        [FunctionName("SaveStore")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (req.Method.ToLower() == "get")
            {
                var res = await ResolveCommand<GetStoresCommand>().Execute(new EmptyRequest());
                var storesStr = string.Join("<br/>", res.Data.Select(JsonConvert.SerializeObject));
                return new OkObjectResult(storesStr);
            }

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<StoreDto>(requestBody, settings);
            var result = await ResolveCommand<SaveStoreCommand>().Execute(data);

            return new OkObjectResult(result);
        }
    }
}
