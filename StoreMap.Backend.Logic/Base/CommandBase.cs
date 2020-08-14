using System;
using StoreMap.Backend.Logic.Helpers;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Logic.Base
{
    public abstract class CommandBase<TRequest, TResponse> : ICommand<TRequest, TResponse>
    {
        protected abstract GenericResponse<TResponse> ExecuteCore(TRequest request);
        
        public GenericResponse<TResponse> Execute(TRequest request)
        {
            try
            {
                return ExecuteCore(request);
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