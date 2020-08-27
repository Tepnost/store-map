using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorStyled;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreMap.Data;
using StoreMap.Logic.Interfaces;
using StoreMap.Logic.Services;
using StoreMap.Util;

namespace StoreMap
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            SetupAuth(builder);
            builder.RootComponents.Add<App>("app");
            
            builder.Services.AddBlazorStyled();
            builder.Services.AddAntDesign();
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IBrowserService, BrowserService>();
            builder.Services.AddScoped<IMessageService, CustomMessageService>();

            await builder.Build().RunAsync();
        }

        private static void SetupAuth(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Local", options.ProviderOptions);
                options.ProviderOptions.DefaultScopes.Add(Constants.RolesClaim);
                options.UserOptions.RoleClaim = Constants.RolesClaim;
            });
            
            builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
            
            builder.Services.AddHttpClient("API", client => client.BaseAddress = new Uri(builder.Configuration["API_URL"]));
            builder.Services
                .AddHttpClient("AUTH_API", client => client.BaseAddress = new Uri(builder.Configuration["API_URL"]))
                .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
        }
    }
}
