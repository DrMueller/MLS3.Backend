using System;

namespace Mmu.Mls3.Common.Areas.Localization.Services
{
    public interface ILocalizationServiceFactory
    {
        ILocalizationService CreateFor(Type type);
    }
}