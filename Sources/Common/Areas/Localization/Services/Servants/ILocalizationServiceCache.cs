using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Mls3.Common.Areas.Localization.Services.Servants
{
    internal interface ILocalizationServiceCache
    {
        FunctionResult<ILocalizationService> TryGetService(string resourceKey);

        void AddServiceToChache(string resourceKey, ILocalizationService service);
    }
}