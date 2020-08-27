using System.Threading.Tasks;

namespace StoreMap.Logic.Interfaces
{
    public interface IMessageService
    {
        Task Success(string content);
        
        Task Error(string content);

        Task Info(string content);
    }
}