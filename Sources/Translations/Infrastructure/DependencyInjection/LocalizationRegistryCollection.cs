using Lamar;
using Mmu.Mls3.Localization.Areas.Localization.Services;
using Mmu.Mls3.Localization.Areas.Services.Implementation;
using Mmu.Mls3.Localization.Areas.Services.Servants;
using Mmu.Mls3.Localization.Areas.Services.Servants.Implementation;

namespace Mmu.Mls3.Localization.Infrastructure.DependencyInjection
{
    public class LocalizationRegistryCollection : ServiceRegistry
    {
        public LocalizationRegistryCollection()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<LocalizationRegistryCollection>();
                scanner.WithDefaultConventions();
            });

            For<ILocalizationServiceFactory>().Use<LocalizationServiceFactory>().Singleton();
            For<ILocalizationServiceCache>().Use<LocalizationServiceCache>().Singleton();
        }
    }
}