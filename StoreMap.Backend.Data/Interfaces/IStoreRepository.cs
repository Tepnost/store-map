using System.Collections.Generic;
using System.Threading.Tasks;
using StoreMap.Backend.Data.Entities;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Data.Interfaces
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllStores();

        Task<Store> SaveStore(StoreDto dto);
    }
}