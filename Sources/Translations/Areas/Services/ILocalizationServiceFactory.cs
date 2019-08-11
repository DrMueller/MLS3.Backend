using System;

namespace Mmu.Mls3.Localization.Areas.Localization.Services
{
    public interface ILocalizationServiceFactory
    {
        ILocalizationService CreateFor(Type type);
    }
}