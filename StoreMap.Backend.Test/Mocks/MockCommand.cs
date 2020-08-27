using System.Threading.Tasks;
using StoreMap.Backend.Logic.Base;
using StoreMap.Data.Responses;
using Task = System.Threading.Tasks.Task;

namespace StoreMap.Backend.Test.Mocks
{
    public class MockCommand : AsyncCommandBase<object, object>
    {
        protected override Task<GenericResponse<object>> ExecuteCore(object request)
        {
            return Task.FromResult(new GenericResponse<object>());
        }
    }
}