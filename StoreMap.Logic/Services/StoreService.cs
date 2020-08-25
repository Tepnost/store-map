using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;
using StoreMap.Data;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;
using StoreMap.Logic.Extensions;
using StoreMap.Logic.ServiceContracts;

namespace StoreMap.Logic.Services
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient client;
        private List<StoreDto> storeCache;

        public StoreService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("API");
            storeCache = new List<StoreDto>();
        }

        public async Task<StoreDto> GetStore(Guid id)
        {
            var store = storeCache.FirstOrDefault(x => x.Id == id);
            if (store != null)
            {
                return store;
            }
            
            var result = await client.GetAsync($"store/{id}");
            store = await result.GetContentDataAsObject<StoreDto>();
            storeCache.Add(store);
            return store;
        }

        public Task<List<StoreDto>> GetAllStores()
        {
            return SafeExecute(async () =>
            {
                var result = await client.GetAsync($"store");
                storeCache = await result.GetContentDataAsObject<List<StoreDto>>();
                return storeCache;
            });
        }

        public async Task<GenericResponse<StoreDto>> SaveStore(StoreDto data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data, Settings.JsonSerializerSettings));
            var result = await client.PostAsync($"store", content);
            var response = await result.GetContentAsObject<StoreDto>();

            if (response.Success)
            {
                SaveInCache(response.Data);
            }

            return response;
        }

        private async Task<T> SafeExecute<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                return default;
            }
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