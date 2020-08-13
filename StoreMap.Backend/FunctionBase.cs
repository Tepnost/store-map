using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.Responses;

namespace StoreMap.Backend
{
    public class FunctionBase
    {
        private readonly IServiceProvider serviceProvider;

        protected FunctionBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected TCommand ResolveCommand<TCommand>() where TCommand : ICommandBase
        {
            var command = serviceProvider.GetService(typeof(TCommand));

            if (command == null)
            {
                throw new ApplicationException($"{typeof(TCommand)} Command not found");
            }

            return (TCommand) command;
        }

        protected async Task<IActionResult> SafeExecute<T>(Func<Task<GenericResponse<T>>> action)
        {
            try
            {
                var result = await action();
                return new StatusWithDataResult(result.StatusCode, result);
            }
            catch (HttpResponseException e)
            {
                return new StatusWithDataResult(e.Response.StatusCode);
            }
        }
    }
}