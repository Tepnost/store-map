using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Logic.Commands
{
    public class GetStoresCommand : AsyncCommandBase<EmptyRequest, List<StoreDto>>
    {
        private readonly IStoreRepository storeRepository;

        public GetStoresCommand(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        protected override async Task<GenericResponse<List<StoreDto>>> ExecuteCore(EmptyRequest request)
        {
            var stores = await storeRepository.GetAllStores();
            var storeDtos = stores.Select(x => x.ToDto()).ToList();

            return GenericResponse<List<StoreDto>>.AsSuccess(storeDtos);
        }
    }
}