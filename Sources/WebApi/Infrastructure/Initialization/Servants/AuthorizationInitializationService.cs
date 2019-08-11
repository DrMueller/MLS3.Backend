using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Mmu.Mls3.WebApi.Infrastructure.Initialization.Servants
{
    internal static class AuthorizationInitializationService
    {
        internal static void Initialize(IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Management", cfg =>
                {
                    cfg.RequireClaim(ClaimTypes.NameIdentifier);
                });
            });
        }
    }
}