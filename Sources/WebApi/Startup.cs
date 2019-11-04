using System.Diagnostics.CodeAnalysis;
using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Mmu.Mls3.WebApi.Infrastructure.DropboxLocation;
using Mmu.Mls3.WebApi.Infrastructure.Initialization;

namespace Mmu.Mls3.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            var dropboxPath = DropboxLocator.LocateDropboxSettingsPath();
            builder.AddJsonFile(
                dropboxPath.IsSuccess ? dropboxPath.Value : "appsettings.json",
                optional: false,
                reloadOnChange: true);

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