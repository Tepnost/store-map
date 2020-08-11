using System.Collections.Generic;
using System.Threading.Tasks;
using StoreMap.Backend.Data.Entities;

namespace StoreMap.Backend.Data.Interfaces
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllStores();
    }
}