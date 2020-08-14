using System;
using System.Threading.Tasks;
using StoreMap.Backend.Logic.Helpers;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Logic.Base
{
    public abstract class AsyncCommandBase<TRequest, TResponse> : IAsyncCommand<TRequest, TResponse>
    {
        protected abstract Task<GenericResponse<TResponse>> ExecuteCore(TRequest request);

        public async Task<GenericResponse<TResponse>> Execute(TRequest request)
        {
            try
            {
                return await ExecuteCore(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                var response = new GenericResponse<TResponse> {Success = false};

                CommandHelper.AddErrorMessageIfDebug(e, response);

                return response;
            }
        }
    }
}