using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using StoreMap.Data.Dtos;
using StoreMap.Logic.Extensions;
using StoreMap.Logic.ServiceContracts;

namespace StoreMap.Logic.Services
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient client;
        private readonly string apiUrl = "http://localhost:7071/api";

        public StoreService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<StoreDto> GetStore(Guid id)
        {
            var result = await client.GetAsync($"{apiUrl}/store/{id}");
            return await result.GetContentDataAsObject<StoreDto>();
        }

        public async Task<List<StoreDto>> GetAllStores()
        {
            var result = await client.GetAsync($"{apiUrl}/store");
            return await result.GetContentDataAsObject<List<StoreDto>>();
        }
    }
}