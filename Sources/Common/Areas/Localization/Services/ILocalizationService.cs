namespace Mmu.Mls3.Common.Areas.Localization.Services
{
    public interface ILocalizationService
    {
        string Localize(string name, params object[] arguments);
    }
}
