using System.Threading.Tasks;
using StoreMap.Data.Dtos;

namespace StoreMap.Logic.ServiceContracts
{
    public interface IBrowserService
    {
        Task<RectPosition> GetElementPosition(string id);
    }
}