using StoreMap.Backend.Logic.Base;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Test.Mocks
{
    public class MockCommand : CommandBase<object, object>
    {
        protected override GenericResponse<object> ExecuteCore(object request)
        {
            return new GenericResponse<object>();
        }
    }
}