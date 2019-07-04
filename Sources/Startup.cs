using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Mmu.Mls3.WebApi.Infrastructure.Initialization;

namespace Mmu.Mls3.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AppInitializationService.InitializeApp(app, env);
        }

        public void ConfigureContainer(ServiceRegistry services)
        {
            ServiceInitializationService.InitializeServices(services, Configuration);
        }
    }
}