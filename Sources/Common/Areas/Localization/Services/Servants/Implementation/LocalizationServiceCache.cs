using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Mls3.Common.Areas.Localization.Services.Servants.Implementation
{
    internal class LocalizationServiceCache : ILocalizationServiceCache
    {
        private readonly IDictionary<string, ILocalizationService> _chache = new Dictionary<string, ILocalizationService>();

        public void AddServiceToChache(string resourceKey, ILocalizationService service)
        {
            _chache.Add(resourceKey, service);
        }

        public FunctionResult<ILocalizationService> TryGetService(string resourceKey)
        {
            var cacheResult = _chache.TryGetValue(resourceKey, out var service);
            if (cacheResult)
            {
                return FunctionResult.CreateSuccess(service);
            }

            return FunctionResult.CreateFailure<ILocalizationService>();
        }
    }
}