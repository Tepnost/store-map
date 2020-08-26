using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;
using StoreMap.Data;
using StoreMap.Data.Dtos;
using StoreMap.Data.Exceptions;
using StoreMap.Data.Responses;
using StoreMap.Logic.Extensions;
using StoreMap.Logic.ServiceContracts;

namespace StoreMap.Logic.Services
{
    public class StoreService : ApiServiceBase, IStoreService
    {
        private readonly HttpClient client;
        private readonly HttpClient authClient;
        private List<StoreDto> storeCache;

        public StoreService(IHttpClientFactory clientFactory, IMessageService messageService) : base(messageService)
        {
            authClient = clientFactory.CreateClient("AUTH_API");
            client = clientFactory.CreateClient("API");
            storeCache = new List<StoreDto>();
        }

        public Task<StoreDto> GetStore(Guid id)
        {
            return SafeExecute(async () =>
            {
                var store = storeCache.FirstOrDefault(x => x.Id == id);
                if (store != null)
                {
                    return store;
                }
                
                var result = await client.GetAsync($"store/{id}");
                store = await GetContentDataAsObject<StoreDto>(result);
                storeCache.Add(store);
                return store;
            });
        }

        public Task<List<StoreDto>> GetAllStores()
        {
            return SafeExecute(async () =>
            {
                var result = await client.GetAsync($"store");
                storeCache = await GetContentDataAsObject<List<StoreDto>>(result);
                return storeCache;
            });
        }

        public Task<GenericResponse<StoreDto>> SaveStore(StoreDto data)
        {
            return SafeExecute(async () =>
            {
                var content = new StringContent(JsonConvert.SerializeObject(data, Settings.JsonSerializerSettings));
                var result = await authClient.PostAsync($"store", content);
                var response = await GetContentAsObject<StoreDto>(result);

                if (response.Success)
                {
                    SaveInCache(response.Data);
                }

                HandleResponseMessage(response);
                return response;
            });
        }

        public Task<bool> DeleteStore(Guid id)
        {
            return SafeExecute(async () =>
            {
                var result = await authClient.DeleteAsync($"store/{id}");
                var response = await GetContentDataAsObject<bool>(result);
                if (response)
                {
                    RemoveFromCache(id);
                }
                
                return response;
            });
        }

        private async Task<T> SafeExecute<T>(Func<Task<T>> action) // TODO: catch these somewhere else, globally
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
            catch (FailedApiRequestException<T> exception)
            {
                HandleResponseMessage(exception.Response);
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
        
        private void RemoveFromCache(Guid id)
        {
            var index = storeCache.FindIndex(x => x.Id == id);
            if (index != -1)
            {
                storeCache.RemoveAt(index);
            }
        }
    }
}