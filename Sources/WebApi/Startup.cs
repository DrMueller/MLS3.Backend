using System.Diagnostics.CodeAnalysis;
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

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needed by ASP.Net Core")]
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