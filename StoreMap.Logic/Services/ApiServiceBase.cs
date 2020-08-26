using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StoreMap.Data;
using StoreMap.Data.Exceptions;
using StoreMap.Data.Responses;
using StoreMap.Logic.ServiceContracts;

namespace StoreMap.Logic.Services
{
    public abstract class ApiServiceBase
    {
        private readonly IMessageService messageService;

        protected ApiServiceBase(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        protected void HandleResponseMessage<T>(GenericResponse<T> response)
        {
            if (string.IsNullOrEmpty(response.Message) && response.Success)
            {
                return;
            }
            
            if (response.Success)
            {
                messageService.Success(response.Message);
            }
            else
            {
                messageService.Error(response.Message ?? Constants.GenericError);
            }
        }
        
        protected async Task<GenericResponse<T>> GetContentAsObject<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GenericResponse<T>>(content, Settings.JsonSerializerSettings);
        }
        
        protected async Task<T> GetContentDataAsObject<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var genericResponse = JsonConvert.DeserializeObject<GenericResponse<T>>(content, Settings.JsonSerializerSettings);

            if (genericResponse.Success == false)
            {
                throw new FailedApiRequestException<T>(genericResponse);
            }
            
            HandleResponseMessage(genericResponse);
            return genericResponse.Data;
        }
    }
}