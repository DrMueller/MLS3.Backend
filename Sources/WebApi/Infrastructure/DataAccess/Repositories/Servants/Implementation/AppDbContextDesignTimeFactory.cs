using System;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mmu.Mls3.WebApi.Infrastructure.KeyVaults;
using Mmu.Mls3.WebApi.Infrastructure.Settings.Models;

namespace Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Servants.Implementation
{
    public class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = ReadConnectionString();
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .ConfigureWarnings(f => f.Throw())
                .Options;

            return new AppDbContext(options);
        }

        // We really need this code only locally, as the scripts are applied via SQL, so we pretty much us the local connection string?
        private static string ReadConnectionString()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var currentDir = Directory.GetCurrentDirectory();

            var configRoot = new ConfigurationBuilder()
                .SetBasePath(currentDir)
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .Build();

            var section = configRoot.GetSection(AppSettings.SectionKey);
            var settings = new ServiceCollection()
                .Configure<AppSettings>(section)
                .AddSingleton(configRoot)
                .BuildServiceProvider()
                .GetService<IOptions<AppSettings>>();

            var keyVaultPath = settings.Value.ConnectionStringKeyVaultPath;
            var readResult = KeyVaultProvider.TryProvidingSecret(keyVaultPath);

            if (!readResult.IsSuccess)
            {
                // This means we're on the build server, which actually doesn't need the connection string
                var tra = Environment.GetEnvironmentVariable("Mls3ConnectionString");
                Console.WriteLine(tra);
                Debug.WriteLine(tra);
                return tra;
            }

            return readResult.Value;
        }
    }
}