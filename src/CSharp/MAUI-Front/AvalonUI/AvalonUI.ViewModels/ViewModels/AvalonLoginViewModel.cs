using AvalonUI.Helpers;
using EasyMicroservices.Security;
using EasyMicroservices.UI.Cores.Navigations;
using EasyMicroservices.UI.Identity.ViewModels.Authentications;
using Identity.GeneratedServices;
using System.Net.Http.Headers;
using System.Text;

namespace AvalonUI.ViewModels;
public class AvalonLoginViewModel : LoginViewModel
{
    public AvalonLoginViewModel(AuthenticationClient authenticationClient, HttpClient httpClient, ISecurityProvider securityProvider) : base(authenticationClient, httpClient, securityProvider)
    {
        _ = ApplicationDataManager.Current.Load(LoadAppData);
    }

    public override Task Load()
    {
        WhiteLabelKey = "5D706148-613A-4C0E-ABB3-ECB41CBD65F6";
        return Task.CompletedTask;
    }

    public override Task Register()
    {
        return NavigationManagerBase.Current.PushAsync(PagesConstants.RegisterPage);
    }

    public override async Task OnLoggedIn(bool isLogin, string token)
    {
        if (isLogin)
        {
            ClientManager.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //save data to local storage
            ApplicationDataManager.Current.ApplicationData.UserName = UserName;
            ApplicationDataManager.Current.ApplicationData.Password = Password;
            await ApplicationDataManager.Current.Save();

            //go to next page
            await NavigationManagerBase.Current.PushAsync(PagesConstants.MainMenuPage);
        }
        else
            await DisplayError("نام کاربری یا رمز عبور صحیح نمی‌باشد.");
    }

    static bool DidFirstLogin = false;
    async Task LoadAppData()
    {
        UserName = ApplicationDataManager.Current.ApplicationData.UserName;
        Password = ApplicationDataManager.Current.ApplicationData.Password;
        OnPropertyChanged(nameof(UserName));
        OnPropertyChanged(nameof(Password));
        if (!DidFirstLogin && UserName.HasValue() && Password.HasValue())
        {
            DidFirstLogin = true;
            await LoginCommand.ExecuteAsync(null);
        }
    }
}
