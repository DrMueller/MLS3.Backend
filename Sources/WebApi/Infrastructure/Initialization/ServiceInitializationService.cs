using System.Linq;
using AutoMapper;
using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mmu.Mls3.Common.Areas.Localization;
using Mmu.Mls3.WebApi.Infrastructure.Initialization.Servants;
using Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Entities;
using Mmu.Mls3.WebApi.Infrastructure.Security.Native;
using Mmu.Mls3.WebApi.Infrastructure.Settings.Models;

namespace Mmu.Mls3.WebApi.Infrastructure.Initialization
{
    internal static class ServiceInitializationService
    {
        internal static void InitializeServices(ServiceRegistry services, IConfiguration config)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services
                .AddIdentityCore<AppUser>()
                .AddUserStore<AppUserStore>();

            services.AddAutoMapper(typeof(ServiceInitializationService).Assembly);
            services.Configure<AppSettings>(config.GetSection(AppSettings.SectionKey));
            InitializeCors(services);
            InitializeSecurity(services, config);
            InitializeLocalisation(services);

            services.Scan(scanner =>
            {
                scanner.AssembliesFromApplicationBaseDirectory();
                scanner.LookForRegistries();
            });
        }

        private static void InitializeCors(ServiceRegistry services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        "All",
                        builder =>
                            builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials());
                });
        }

        private static void InitializeLocalisation(ServiceRegistry services)
        {
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    opts.DefaultRequestCulture = new RequestCulture("en");
                    opts.SupportedCultures = SupportedCultures.All.Value.ToList();
                    opts.SupportedUICultures = SupportedCultures.All.Value.ToList();
                });
        }

        private static void InitializeSecurity(ServiceRegistry services, IConfiguration config)
        {
            AuthenticationInitializationService.Initialize(services, config);
            AuthorizationInitializationService.Initialize(services);
        }
    }
}