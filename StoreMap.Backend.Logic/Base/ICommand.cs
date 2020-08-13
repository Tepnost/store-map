using System.Threading.Tasks;
using StoreMap.Backend.Logic.Responses;

namespace StoreMap.Backend.Logic.Base
{
    public interface ICommand<in TRequest, TResponse> : ICommandBase
    {
        GenericResponse<TResponse> Execute(TRequest request);
    }
    
    public interface IAsyncCommand<in TRequest, TResponse> : ICommandBase
    {
        Task<GenericResponse<TResponse>> Execute(TRequest request);
    }
}