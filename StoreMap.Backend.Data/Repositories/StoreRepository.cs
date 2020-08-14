using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly IMongoCollection<Store> storesCollection;
        
        public StoreRepository()
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDBConnectionString"));
            var database = client.GetDatabase("store_map");
            storesCollection = database.GetCollection<Store>("stores");
        }

        public async Task<List<Store>> GetAllStores()
        {
            var stores = await storesCollection.FindAsync(x => true);

            return stores.ToList();
        }

        public async Task<Store> SaveStore(StoreDto dto)
        {
            var store = Store.FromDto(dto);
            
            await storesCollection.InsertOneAsync(store);

            return store;
        }

        public async Task<Store> FindStore(Guid id)
        {
            var stores = await storesCollection.FindAsync(x => x.Id == id);
            return stores.FirstOrDefault();
        }
    }
}