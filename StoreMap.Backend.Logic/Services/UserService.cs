using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using StoreMap.Backend.Logic.ServiceContracts;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Logic.Services
{
    public class UserService : IUserService
    {
        private static readonly string UserInfoUrl = "https://store-map.eu.auth0.com/userinfo";
        
        private readonly HttpClient client;

        public UserService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<UserData> GetUserData(string accessToken)
        {
            var userData = await client.GetFromJsonAsync<UserData>($"{UserInfoUrl}?access_token={accessToken}");
            return userData;
        }
    }
}