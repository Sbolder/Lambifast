using Lambifast.Services.Impl;
using Lambifast.Services;
using Microsoft.Extensions.Localization;

namespace Lambifast.Localization
{
    public static class LocalizationExtensions
    {
        public static void UseLocalizationService(this WebApplication app, params string[] supportedLanguages)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures(supportedLanguages)
                    .AddSupportedUICultures(supportedLanguages)
                    .SetDefaultCulture(supportedLanguages.First());
            });
        }

        public static void AddLocalizationService(this IServiceCollection services, string assemblyName, string resourceName)
        {
            // Enable .NET Localization
            services.AddLocalization();

            services.AddSingleton<ILocalizationService, LocalizationService>(x => new LocalizationService(
                x.GetService<IStringLocalizerFactory>(),
                assemblyName,
                resourceName));
        }
    }

}
