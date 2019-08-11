using Microsoft.Extensions.Localization;
using Mmu.Mls3.Localization.Areas.Localization.Services;

namespace Mmu.Mls3.Localization.Areas.Services.Implementation
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