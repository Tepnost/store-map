using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Logic.Commands
{
    public class GetStoresCommand : AsyncCommandBase<SearchRequest, List<StoreDto>>
    {
        private readonly IStoreRepository storeRepository;

        public GetStoresCommand(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        protected override async Task<GenericResponse<List<StoreDto>>> ExecuteCore(SearchRequest request)
        {
            var stores = string.IsNullOrEmpty(request.SearchTerm)
                ? await storeRepository.GetAll()
                : await storeRepository.GetAll(request.SearchTerm?.ToLowerInvariant().Trim());
            
            var storeDtos = stores.Select(x => x.ToDto()).ToList();

            return GenericResponse<List<StoreDto>>.AsSuccess(storeDtos);
        }
    }
}