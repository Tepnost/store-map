using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;

namespace StoreMap.Logic.ServiceContracts
{
    public interface IStoreService
    {
        Task<StoreDto> GetStore(Guid id);

        Task<List<StoreDto>> GetAllStores();

        Task<GenericResponse<StoreDto>> SaveStore(StoreDto data);
    }
}