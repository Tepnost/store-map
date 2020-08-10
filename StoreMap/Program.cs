using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorStyled;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StoreMap.Logic.ServiceContracts;
using StoreMap.Logic.Services;

namespace StoreMap
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            
            builder.Services.AddBlazorStyled();
            builder.Services.AddAntDesign();
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IStoreObjectService, StoreObjectService>();
            builder.Services.AddScoped<IBrowserService, BrowserService>();

            await builder.Build().RunAsync();
        }
    }
}
