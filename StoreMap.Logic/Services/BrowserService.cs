using System.Threading.Tasks;
using Microsoft.JSInterop;
using StoreMap.Data.Dtos;
using StoreMap.Logic.ServiceContracts;

namespace StoreMap.Logic.Services
{
    public class BrowserService : IBrowserService
    {
        private readonly IJSRuntime js;

        public BrowserService(IJSRuntime js)
        {
            this.js = js;
        }

        public async Task<RectPosition> GetElementPosition(string id)
        {
            return await js.InvokeAsync<RectPosition>("blazor_getElementPosition", id);
        }

        public async Task GoBack()
        {
            await js.InvokeVoidAsync("history.back");
        }
    }
}