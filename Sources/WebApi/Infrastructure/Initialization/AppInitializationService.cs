using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Mmu.Mls3.Localization.Areas;
using Mmu.Mls3.WebApi.Infrastructure.Middlewares;

namespace Mmu.Mls3.WebApi.Infrastructure.Initialization
{
    public static class AppInitializationService
    {
        public static void InitializeApp(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseCors("All");
            app.UseHttpsRedirection();
            app.UseAuthentication();

            InitializeLocalization(app);

            app.UseMvc();
        }

        private static void InitializeLocalization(IApplicationBuilder app)
        {
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = SupportedCultures.All.Value.ToList(),
                SupportedUICultures = SupportedCultures.All.Value.ToList()
            });
        }
    }
}