using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Mls3.Common.Areas.Localization.Services.Servants
{
    internal interface ILocalizationServiceCache
    {
        void AddServiceToChache(string resourceKey, ILocalizationService service);

        FunctionResult<ILocalizationService> TryGetService(string resourceKey);
    }
}