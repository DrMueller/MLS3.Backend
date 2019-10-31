namespace Mmu.Mls3.WebApi.Infrastructure.Settings.Models
{
    public class AppSettings
    {
        public const string SectionKey = "AppSettings";

        public string ConnectionStringKeyVaultPath { get; set; }
        public string SecretKey { get; set; }
        public bool AutoMigrateDatabase { get; set; }
    }
}