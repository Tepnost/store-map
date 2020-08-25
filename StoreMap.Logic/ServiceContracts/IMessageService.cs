using System.Threading.Tasks;

namespace StoreMap.Logic.ServiceContracts
{
    public interface IMessageService
    {
        Task Success(string content);
        
        Task Error(string content);

        Task Info(string content);
    }
}