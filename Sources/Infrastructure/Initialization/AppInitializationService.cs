using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Mmu.Mls3.WebApi.Infrastructure.Middlewares;

namespace Mmu.Mls3.WebApi.Infrastructure.Initialization
{
    public static class AppInitializationService
    {
        public static void InitializeApp(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseCors("All");
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseAuthentication();
        }
    }
}