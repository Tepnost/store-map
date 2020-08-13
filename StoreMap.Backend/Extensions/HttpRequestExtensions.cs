using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StoreMap.Data;

namespace StoreMap.Backend.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> GetBodyAsObject<T>(this HttpRequest req)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<T>(requestBody, Settings.JsonSerializerSettings);
                return data;
            }
            catch (JsonSerializationException e)
            {
                Console.WriteLine(e);
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}