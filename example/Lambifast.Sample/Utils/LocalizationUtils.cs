using Lambifast.Services;

namespace Lambifast.Sample.Utils
{
    public class LocalizationUtils
    {
        public static string localizeResponse(ILocalizationService LocalizationService, string code)
        {
            return LocalizationService.GetLocalized($"{code}");
        }
    }
}
