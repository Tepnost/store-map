using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Logic.Commands;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Backend.Logic.Responses;

namespace StoreMap.Backend
{
    public class SaveStore : FunctionBase
    {
        public SaveStore(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        
        [FunctionName("SaveStore")]
        public Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            return SafeExecute(() => Run(req));
        }

        private async Task<GenericResponse<List<Store>>> Run(HttpRequest req)
        {
            // if (req.Method.ToLower() == "get")
            // {
                return await ResolveCommand<GetStoresCommand>().Execute(new EmptyRequest());
            // }
                
            // var data = await req.GetBodyAsObject<StoreDto>();
            // return await ResolveCommand<SaveStoreCommand>().Execute(data);
        }
    }
}
