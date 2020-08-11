using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Data.Repositories;

[assembly: FunctionsStartup(typeof(StoreMap.Backend.Startup))]

namespace StoreMap.Backend
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IStoreRepository, StoreRepository>();
        }
    }
}