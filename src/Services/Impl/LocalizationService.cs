using Microsoft.Extensions.Localization;

namespace Lambifast.Services.Impl
{
    public class LocalizationService : ILocalizationService
    {
        private IStringLocalizer StringLocalizer { get; }

        public LocalizationService(
            IStringLocalizerFactory stringLocalizerFactory,
            string assemblyName,
            string resourceName)
        {
            StringLocalizer = stringLocalizerFactory.Create(resourceName, assemblyName);
        }

        public string GetLocalized(string code)
        {
            var localizedString = StringLocalizer[code];
            return localizedString;
        }
    }
}
