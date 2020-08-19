using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;

namespace StoreMap.Util
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(
            IAccessTokenProvider provider,
            IConfiguration configuration,
            NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            ConfigureHandler(new[] { configuration["API_URL"] });
        }
    }
}