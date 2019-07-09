using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Options;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Servants;
using Mmu.Mls3.WebApi.Infrastructure.Settings.Models;

namespace Mmu.Mls3.WebApi.Infrastructure.Middlewares
{
    internal class DbMigrationMiddleware
    {
        private readonly Option<AppSettings> _appSettings;
        private readonly IDbContextFactory _dbContextFactoy;
        private readonly RequestDelegate _next;

        public DbMigrationMiddleware(
            RequestDelegate next,
            IDbContextFactory dbContextFactoy,
            Option<AppSettings> appSettings)
        {
            _next = next;
            _dbContextFactoy = dbContextFactoy;
            _appSettings = appSettings;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (_appSettings.OptionValue.AutoMigrateDatabase)
            {
                var dbContext = _dbContextFactoy.Create();
                dbContext.Database.Migrate();
                await _next(httpContext);
            }
        }
    }
}