using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreMap.Backend.Data.Entities;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Data.Interfaces
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAll(string searchTerm);

        Task<List<Store>> GetAll();

        Task<Store> Insert(Store store);
        
        Task<Store> Update(Store store);

        Task<Store> FindOne(Guid id);

        Task<bool> Delete(Guid id);
    }
}