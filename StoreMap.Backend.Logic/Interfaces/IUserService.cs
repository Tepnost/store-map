using System.Threading.Tasks;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Logic.Interfaces
{
    public interface IUserService
    {
        Task<UserData> GetUserData(string accessToken);
    }
}