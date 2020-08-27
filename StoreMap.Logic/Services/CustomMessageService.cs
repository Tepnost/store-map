using System.Threading.Tasks;
using AntDesign;
using StoreMap.Logic.Interfaces;

namespace StoreMap.Logic.Services
{
    public class CustomMessageService : IMessageService
    {
        private readonly MessageService message;

        public CustomMessageService(MessageService message)
        {
            this.message = message;
        }

        public Task Success(string content)
        {
            return message.Success(content);
        }

        public Task Error(string content)
        {
            return message.Error(content);
        }

        public Task Info(string content)
        {
            return message.Info(content);
        }
    }
}