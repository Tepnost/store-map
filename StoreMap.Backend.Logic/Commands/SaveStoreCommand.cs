using System.Threading.Tasks;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.Responses;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Logic.Commands
{
    public class SaveStoreCommand : AsyncCommandBase<StoreDto, Store>
    {
        private readonly IStoreRepository storeRepository;

        public SaveStoreCommand(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }
        
        protected override async Task<GenericResponse<Store>> ExecuteCore(StoreDto request)
        {
            var store = await storeRepository.SaveStore(request);

            return GenericResponse<Store>.AsSuccess(store);
        }
    }
}