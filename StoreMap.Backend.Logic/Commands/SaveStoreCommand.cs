using System.Threading.Tasks;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Base;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Logic.Commands
{
    public class SaveStoreCommand : AsyncCommandBase<StoreDto, StoreDto>
    {
        private readonly IStoreRepository storeRepository;

        public SaveStoreCommand(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }
        
        protected override async Task<GenericResponse<StoreDto>> ExecuteCore(StoreDto request)
        {
            var store = await storeRepository.SaveStore(request);

            return GenericResponse<StoreDto>.AsSuccess(store.ToDto());
        }
    }
}