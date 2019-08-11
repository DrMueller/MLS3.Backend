using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;
using Mmu.Mls3.Localization.Areas.Localization.Services;

namespace Mmu.Mls3.Localization.Areas.Services.Servants
{
    internal interface ILocalizationServiceCache
    {
        FunctionResult<ILocalizationService> TryGetService(string resourceKey);

        void AddServiceToChache(string resourceKey, ILocalizationService service);
    }
}