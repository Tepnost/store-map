using System.Threading.Tasks;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Logic.Commands
{
    public class GetStoreCommand : AsyncCommandBase<GetByGuidRequest, StoreDto>
    {
        private readonly IStoreRepository storeRepository;

        public GetStoreCommand(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        protected override async Task<GenericResponse<StoreDto>> ExecuteCore(GetByGuidRequest request)
        {
            var stores = await storeRepository.FindOne(request.Id);
            
            return GenericResponse<StoreDto>.AsSuccess(stores.ToDto());
        }
    }
}