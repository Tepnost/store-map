using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Data.Repositories;
using StoreMap.Backend.Extensions;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.ServiceContracts;
using StoreMap.Backend.Logic.Services;

[assembly: FunctionsStartup(typeof(StoreMap.Backend.Startup))]

namespace StoreMap.Backend
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IStoreRepository, StoreRepository>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.RegisterAllImplenetations(typeof(ICommandBase));
        }
    }
}