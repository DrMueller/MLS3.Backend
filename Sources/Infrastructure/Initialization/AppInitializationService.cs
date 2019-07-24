using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            app.UseMvc();
        }
    }
}