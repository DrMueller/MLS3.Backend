using Microsoft.Extensions.Localization;

namespace Mmu.Mls3.Common.Areas.Localization.Services.Implementation
{
    internal class LocalizationService : ILocalizationService
    {
        private readonly IStringLocalizer _stringLocalizer;

        public LocalizationService(IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public string Localize(string name, params object[] arguments)
        {
            return _stringLocalizer.GetString(name, arguments);
        }
    }
}