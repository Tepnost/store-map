using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StoreMap.Data;
using StoreMap.Data.Dtos;
using StoreMap.Logic.Extensions;
using StoreMap.Logic.ServiceContracts;

namespace StoreMap.Logic.Services
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient client;
        private readonly string apiUrl = "http://localhost:7071/api";
        private List<StoreDto> storeCache;

        public StoreService(HttpClient client)
        {
            this.client = client;
            storeCache = new List<StoreDto>();
        }

        public async Task<StoreDto> GetStore(Guid id)
        {
            var store = storeCache.FirstOrDefault(x => x.Id == id);
            if (store != null)
            {
                return store;
            }
            
            var result = await client.GetAsync($"{apiUrl}/store/{id}");
            store = await result.GetContentDataAsObject<StoreDto>();
            storeCache.Add(store);
            return store;
        }

        public async Task<List<StoreDto>> GetAllStores()
        {
            var result = await client.GetAsync($"{apiUrl}/store");
            storeCache = await result.GetContentDataAsObject<List<StoreDto>>();
            return storeCache;
        }

        public async Task<StoreDto> SaveStore(StoreDto data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data, Settings.JsonSerializerSettings));
            var result = await client.PostAsync($"{apiUrl}/store", content);
            var store = await result.GetContentDataAsObject<StoreDto>();
            SaveInCache(store);

            return store;
        }

        private void SaveInCache(StoreDto store)
        {
            var index = storeCache.FindIndex(x => x.Id == store.Id);
            if (index == -1)
            {
                storeCache.Add(store);
            }
            else
            {
                storeCache[index] = store;
            }
        }
    }
}