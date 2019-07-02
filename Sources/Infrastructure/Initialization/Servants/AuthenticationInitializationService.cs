using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Mmu.Mls3.WebApi.Infrastructure.Security.Handlers;

namespace Mmu.Mls3.WebApi.Infrastructure.Initialization.Servants
{
    internal static class AuthenticationInitializationService
    {
        internal static void Initialize(IServiceCollection services)
        {
            services
                .AddAuthentication("App")
                .AddScheme<AuthenticationSchemeOptions, AppAuthenticationHandler>("App", null);
        }
    }
}