using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.ConnectionStrings;
using Mmu.Mls3.WebApi.Infrastructure.Settings.Models;

namespace Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Servants.Implementation
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IOptions<AppSettings> _appSettings;

        public DbContextFactory(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public DbContext Create()
        {
            var connectionString = KeyVaultProvider.ProvideSecret(_appSettings.Value.ConnectionStringKeyVaultPath);

            var options = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .ConfigureWarnings(f => f.Throw())
                .Options;

            var result = new AppDbContext(options);
            return result;
        }
    }
}