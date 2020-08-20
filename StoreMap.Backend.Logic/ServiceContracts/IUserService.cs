using System.Threading.Tasks;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Logic.ServiceContracts
{
    public interface IUserService
    {
        Task<UserData> GetUserData(string accessToken);
    }
}