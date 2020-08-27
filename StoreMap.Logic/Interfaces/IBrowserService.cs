using System.Threading.Tasks;
using StoreMap.Data.Dtos;

namespace StoreMap.Logic.Interfaces
{
    public interface IBrowserService
    {
        Task<RectPosition> GetElementPosition(string id);

        Task GoBack();
    }
}