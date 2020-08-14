using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Logic.Commands;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Backend.Util;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Functions
{
    public class GetStore : FunctionBase
    {
        public GetStore(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        
        [FunctionName("GetStore")]
        public Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "store/{id:Guid}")] HttpRequest req, Guid id,
            ILogger log)
        {
            return SafeExecute(() => Run(req, id));
        }
        
        private Task<GenericResponse<StoreDto>> Run(HttpRequest req, Guid id)
        {
            return ResolveCommand<GetStoreCommand>().Execute(new GetByGuidRequest
            {
                Id = id
            });
        }
    }
}
