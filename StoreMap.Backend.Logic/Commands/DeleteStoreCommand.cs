using System.Threading.Tasks;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Logic.Commands
{
    public class DeleteStoreCommand : AsyncCommandBase<GetByGuidRequest, bool>
    {
        private readonly IStoreRepository storeRepository;

        public DeleteStoreCommand(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }
        
        protected override async Task<GenericResponse<bool>> ExecuteCore(GetByGuidRequest request)
        {
            var success = await storeRepository.DeleteStore(request.Id);
            
            return new GenericResponse<bool>
            {
                Success = success,
                Data = success,
                Message = success
                    ? "Store deleted successfully!"
                    : "Could not delete store."
            };
        }
    }
}