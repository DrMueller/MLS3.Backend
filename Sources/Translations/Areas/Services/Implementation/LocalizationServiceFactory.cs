using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mls3.Localization.Areas.Localization.Services;
using Mmu.Mls3.Localization.Areas.Services.Servants;

namespace Mmu.Mls3.Localization.Areas.Services.Implementation
{
    internal class LocalizationServiceFactory : ILocalizationServiceFactory
    {
        private const string BaseAssemblyNamespace = "Mmu.Mls3.";
        private const string BaseResourcePath = BaseAssemblyNamespace + "Localization.Areas.Translations.";
        private readonly ILocalizationServiceCache _cache;
        private readonly object _lock = new object();
        private readonly IReadOnlyCollection<Type> _resourceTypes;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        public LocalizationServiceFactory(
            IStringLocalizerFactory stringLocalizerFactory,
            ILocalizationServiceCache cache)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
            _cache = cache;

            _resourceTypes = typeof(LocalizationServiceFactory)
                .Assembly
                .GetTypes()
                .Where(type => type.FullName.StartsWith(BaseResourcePath, StringComparison.Ordinal))
                .ToList();
        }

        public ILocalizationService CreateFor(Type type)
        {
            var resourceKey = CreateResourceKey(type);
            var cachedService = _cache.TryGetService(resourceKey);

            if (cachedService.IsSuccess)
            {
                return cachedService.Value;
            }

            lock (_lock)
            {
                cachedService = _cache.TryGetService(resourceKey);
                if (cachedService.IsSuccess)
                {
                    return cachedService.Value;
                }

                return CreateAndCache(resourceKey);
            }
        }

        private static string CreateResourceKey(Type type)
        {
            var typeNamewithNamespace = type.FullName.Replace(BaseAssemblyNamespace, string.Empty, StringComparison.Ordinal);
            var resourceKey = BaseResourcePath + typeNamewithNamespace;
            return resourceKey;
        }

        private ILocalizationService CreateAndCache(string resourceKey)
        {
            var resourceTypes = _resourceTypes.Where(f => f.FullName == resourceKey).ToList();
            Guard.That(() => resourceTypes.Count() == 1, $"Found more than one Resource Type for {resourceKey}.");
            Guard.That(() => resourceTypes.Any(), $"No Resource Type for {resourceKey} found.");

            var stringLocalizer = _stringLocalizerFactory.Create(resourceTypes.Single());
            var localizationService = new LocalizationService(stringLocalizer);
            _cache.AddServiceToChache(resourceKey, localizationService);
            return localizationService;
        }
    }
}