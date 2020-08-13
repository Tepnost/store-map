using System;
using System.Linq;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace StoreMap.Backend.Extensions
{
    public static class BuilderExtensions
    {
        public static void RegisterAllImplenetations(this IFunctionsHostBuilder builder, Type baseType)
        {
            var types = baseType
                .Assembly
                .DefinedTypes
                .Where(x => x.GetInterfaces().Contains(baseType) && !x.IsAbstract && !x.IsInterface);
            
            foreach (var type in types)
            {
                builder.Services.Add(new ServiceDescriptor(type.AsType(), type, ServiceLifetime.Scoped));
            }
        }
    }
}