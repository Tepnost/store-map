using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Extensions;
using StoreMap.Backend.Logic.Commands;
using StoreMap.Backend.Util;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Functions
{
    public class SaveStore : FunctionBase
    {
        public SaveStore(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        
        [FunctionName("SaveStore")]
        public Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "store")] HttpRequest req,
            ILogger log)
        {
            return SafeExecute(() => Run(req));
        }

        private async Task<GenericResponse<StoreDto>> Run(HttpRequest req)
        {
            var data = await req.GetBodyAsObject<StoreDto>();
            return await ResolveCommand<SaveStoreCommand>().Execute(data);
        }
    }
}
