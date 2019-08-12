using System;
using Microsoft.Extensions.Localization;
using Mmu.Mls3.Common.Areas.Localization.Services.Servants;

namespace Mmu.Mls3.Common.Areas.Localization.Services.Implementation
{
    internal class LocalizationServiceFactory : ILocalizationServiceFactory
    {
        private readonly ILocalizationServiceCache _cache;
        private readonly object _lock = new object();
        private readonly IResourceTypeFetcher _resourceTypeFetcher;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        public LocalizationServiceFactory(
            IStringLocalizerFactory stringLocalizerFactory,
            IResourceTypeFetcher resourceTypeFetcher,
            ILocalizationServiceCache cache)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
            _resourceTypeFetcher = resourceTypeFetcher;
            _cache = cache;
        }

        public ILocalizationService CreateFor(Type type)
        {
            var resourceKey = _resourceTypeFetcher.CreateResourceKey(type);
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

        private ILocalizationService CreateAndCache(string resourceKey)
        {
            var resourceType = _resourceTypeFetcher.FetchResourceType(resourceKey);

            var stringLocalizer = _stringLocalizerFactory.Create(resourceType);
            var localizationService = new LocalizationService(stringLocalizer);
            _cache.AddServiceToChache(resourceKey, localizationService);
            return localizationService;
        }
    }
}