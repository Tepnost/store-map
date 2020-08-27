using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using StoreMap.Backend.Logic.Commands;
using StoreMap.Backend.Logic.Interfaces;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Backend.Util;
using StoreMap.Data.Enums;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Functions
{
    public class DeleteStore : FunctionBase
    {
        public DeleteStore(IServiceProvider serviceProvider, IUserService userService) : base(serviceProvider, userService)
        {
        }
        
        [FunctionName("DeleteStore")]
        public Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "store/{id:Guid}")] HttpRequest req, Guid id)
        {
            return SafeExecute(() => RunInternal(req, id));
        }
        
        private async Task<GenericResponse<bool>> RunInternal(HttpRequest req, Guid id)
        {
            await ValidateTokenAsync(req, UserRole.AdminMod);
            return await ResolveCommand<DeleteStoreCommand>().Execute(new GetByGuidRequest
            {
                Id = id
            });
        }
    }
}