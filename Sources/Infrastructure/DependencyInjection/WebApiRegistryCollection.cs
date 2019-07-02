using Lamar;

namespace Mmu.Mls3.WebApi.Infrastructure.DependencyInjection
{
    public class WebApiRegistryCollection : ServiceRegistry
    {
        public WebApiRegistryCollection()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<WebApiRegistryCollection>();
                scanner.WithDefaultConventions();
            });
        }
    }
}