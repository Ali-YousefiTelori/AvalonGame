using AvalonUI.Helpers;
using AvalonUI.ViewModels;
using EasyMicroservices.UI.Cores.Navigations;
using EasyMicroservices.UI.Identity.ViewModels.Authentications;
using EasyMicroservices.UI.MauiComponents.Navigations;
using Microsoft.Extensions.Logging;

namespace AvalonUI;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                //https://fontawesome.com/icons/ellipsis-vertical?f=classic&s=regular
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-brands-400.ttf", "FontRegular");
                fonts.AddFont("fa-regular-400.ttf", "FontBrands");
                fonts.AddFont("fa-solid-900.ttf", "FontSolid");
            });
        builder.Services.AddSingleton(sp => ClientManager.AuthenticationClient);
        builder.Services.AddTransient<LoginViewModel, AvalonLoginViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
