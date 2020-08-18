using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreMap.Data.Dtos;

namespace StoreMap.Logic.ServiceContracts
{
    public interface IStoreService
    {
        Task<StoreDto> GetStore(Guid id);

        Task<List<StoreDto>> GetAllStores();

        Task<StoreDto> SaveStore(StoreDto data);
    }
}