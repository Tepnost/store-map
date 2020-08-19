using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorStyled;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreMap.Logic.ServiceContracts;
using StoreMap.Logic.Services;
using StoreMap.Util;

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
            SetupAuth(builder);
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IStoreService, StoreService>();
            builder.Services.AddSingleton<IBrowserService, BrowserService>();

            await builder.Build().RunAsync();
        }

        private static void SetupAuth(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });
            
            builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
            
            builder.Services
                .AddHttpClient("API", client => client.BaseAddress = new Uri(builder.Configuration["API_URL"]))
                .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
            
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("API"));
        }
    }
}
