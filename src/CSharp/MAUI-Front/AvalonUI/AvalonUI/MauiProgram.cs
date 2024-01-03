using AvalonUI.Helpers;
using AvalonUI.ViewModels;
using EasyMicroservices.Domain.Contracts.Common;
using EasyMicroservices.Security;
using EasyMicroservices.Security.Providers.HashProviders;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Navigations;
using EasyMicroservices.UI.Identity.Helpers;
using EasyMicroservices.UI.Identity.ViewModels.Authentications;
using Microsoft.Extensions.Logging;

namespace AvalonUI;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        LoadLanguage("fa-IR");
        BaseViewModel.CurrentApplicationLanguage = "fa-IR";
        BaseViewModel.IsRightToLeft = true;
        BaseViewModel.OnGlobalServiceErrorHandler = async (error) =>
        {
            for (int i = 0; i < 10; i++)
            {
                await NavigationManagerBase.Current.PopAsync();
            }
            await NavigationManagerBase.Current.PushAsync(PagesConstants.LoginPage);
            return false;
        };
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
        builder.Services.AddTransient<RegisterViewModel, AvalonRegisterViewModel>();
        builder.Services.AddScoped<ISecurityProvider, SHA256HashProvider>();
        builder.Services.AddSingleton((sp) => ClientManager.HttpClient);
#if DEBUG
        builder.Logging.AddDebug();
#endif
        ApplicationDataManager.Initialize(FileSystem.Current.AppDataDirectory);
        var build = builder.Build();
        ViewModelLocator.ServiceProvider = build.Services;
        return build;
    }

    static void LoadLanguage(string languageShortName)
    {
        BaseViewModel.AppendLanguage("Username_Title", new LanguageContract()
        {
            ShortName = languageShortName,
            Value = "نام کاربری:"
        });
        BaseViewModel.AppendLanguage("Password_Title", new LanguageContract()
        {
            ShortName = languageShortName,
            Value = "رمز عبور:"
        });
        BaseViewModel.AppendLanguage("ConfirmPassword_Title", new LanguageContract()
        {
            ShortName = languageShortName,
            Value = "تکرار رمز عبور:"
        });
        BaseViewModel.AppendLanguage("Login", new LanguageContract()
        {
            ShortName = languageShortName,
            Value = "ورود"
        });
        BaseViewModel.AppendLanguage("Cancel", new LanguageContract()
        {
            ShortName = languageShortName,
            Value = "انصراف"
        });
        BaseViewModel.AppendLanguage("Register", new LanguageContract()
        {
            ShortName = languageShortName,
            Value = "ثبت نام"
        });
        BaseViewModel.AppendLanguage("Username_Validation_ErrorMessage", new LanguageContract()
        {
            ShortName = languageShortName,
            Value = "نام کاربری باید حداقل 3 حرف داشته باشد"
        });
        BaseViewModel.AppendLanguage("Password_Validation_ErrorMessage", new LanguageContract()
        {
            ShortName = languageShortName,
            Value = "رمز عبور باید حداقل 7 حرف داشته باشد"
        });
        BaseViewModel.AppendLanguage("Password_Not_Match", new LanguageContract()
        {
            ShortName = languageShortName,
            Value = "رمزهای عبور باید یکسان باشند تا مطمئن شویم شما اطلاعات را درست وارد کرده اید. رمز های عبور وارد شده باهم همخوانی ندارند."
        });
    }
}
