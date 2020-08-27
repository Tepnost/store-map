using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Data.Interfaces;

namespace StoreMap.Backend.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class StoreRepository : IStoreRepository, ICrudRepository<Store>
    {
        private readonly IMongoCollection<Store> storesCollection;
        
        public StoreRepository()
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MongoDBConnectionString"));
            var database = client.GetDatabase("store_map");
            storesCollection = database.GetCollection<Store>("stores");
        }

        public async Task<List<Store>> GetAll(string searchTerm)
        {
            var stores = await storesCollection
                .FindAsync(x => x.Name.ToLowerInvariant().StartsWith(searchTerm));

            return stores.ToList();
        }

        public async Task<Store> Update(Store store)
        {
            await storesCollection.FindOneAndUpdateAsync(
                x => x.Id == store.Id,
                new ObjectUpdateDefinition<Store>(store));
            
            return store;
        }

        public async Task<List<Store>> GetAll()
        {
            var stores = await storesCollection.FindAsync(x => true);
            return stores.ToList();
        }

        public async Task<Store> Insert(Store store)
        {
            await storesCollection.InsertOneAsync(store);

            return store;
        }

        public async Task<Store> FindOne(Guid id)
        {
            var stores = await storesCollection.FindAsync(x => x.Id == id);
            return stores.FirstOrDefault();
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await storesCollection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }
    }
}