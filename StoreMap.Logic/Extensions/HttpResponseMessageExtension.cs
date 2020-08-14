using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StoreMap.Data;
using StoreMap.Data.Responses;

namespace StoreMap.Logic.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<GenericResponse<T>> GetContentAsObject<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GenericResponse<T>>(content, Settings.JsonSerializerSettings);
        }
        
        public static async Task<T> GetContentDataAsObject<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var genericResponse = JsonConvert.DeserializeObject<GenericResponse<T>>(content, Settings.JsonSerializerSettings);
            return genericResponse.Data;
        }
    }
}