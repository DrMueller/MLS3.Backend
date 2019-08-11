namespace Mmu.Mls3.Localization.Areas.Localization.Services
{
    public interface ILocalizationService
    {
        string Localize(string name, params object[] arguments);
    }
}
