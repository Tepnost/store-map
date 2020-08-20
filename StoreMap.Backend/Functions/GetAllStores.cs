using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using StoreMap.Backend.Logic.Commands;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Backend.Logic.ServiceContracts;
using StoreMap.Backend.Util;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Functions
{
    public class GetAllStores : FunctionBase
    {
        public GetAllStores(IServiceProvider serviceProvider, IUserService userService) : base(serviceProvider, userService)
        {
        }
        
        [FunctionName("GetAllStores")]
        public Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "store")] HttpRequest req,
            ILogger log)
        {
            return SafeExecute(() => Run(req));
        }
        
        private Task<GenericResponse<List<StoreDto>>> Run(HttpRequest req)
        {
            return ResolveCommand<GetStoresCommand>().Execute(new EmptyRequest());
        }
    }
}
