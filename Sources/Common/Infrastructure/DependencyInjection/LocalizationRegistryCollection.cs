using Lamar;
using Mmu.Mls3.Common.Areas.Localization.Services;
using Mmu.Mls3.Common.Areas.Localization.Services.Implementation;
using Mmu.Mls3.Common.Areas.Localization.Services.Servants;
using Mmu.Mls3.Common.Areas.Localization.Services.Servants.Implementation;

namespace Mmu.Mls3.Common.Infrastructure.DependencyInjection
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

            // Localization
            For<ILocalizationServiceFactory>().Use<LocalizationServiceFactory>().Singleton();
            For<ILocalizationServiceCache>().Use<LocalizationServiceCache>().Singleton();
            For<IResourceTypeFetcher>().Use<ResourceTypeFetcher>().Singleton();
        }
    }
}