using System.Collections.Generic;
using System.Threading.Tasks;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Backend.Logic.Responses;

namespace StoreMap.Backend.Logic.Commands
{
    public class GetStoresCommand : AsyncCommandBase<EmptyRequest, List<Store>>
    {
        private readonly IStoreRepository storeRepository;

        public GetStoresCommand(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        protected override async Task<GenericResponse<List<Store>>> ExecuteCore(EmptyRequest request)
        {
            var stores = await storeRepository.GetAllStores();

            return GenericResponse<List<Store>>.AsSuccess(stores);
        }
    }
}