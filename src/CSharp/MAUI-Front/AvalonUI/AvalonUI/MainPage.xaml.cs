using AvalonUI.Design.Views;
using AvalonUI.Helpers;
using EasyMicroservices.UI.Cores.Navigations;
using EasyMicroservices.UI.Identity.Maui.Design.Views;
using EasyMicroservices.UI.MauiComponents.Design.Pages;
using EasyMicroservices.UI.MauiComponents.Navigations;

namespace AvalonUI;

public partial class MainPage : EasyContentPage
{
    public MainPage()
    {
        InitializeComponent();
        NavigationManagerBase.Current = new DefaultNavigationManager(Navigation);
        RegisterViewAsPage<UserProfilesView>(PagesConstants.ProfilesPage);
        RegisterViewAsPage<ProfileCardViewer>(PagesConstants.ProfileCardViewerPage);
        RegisterViewAsPage<MissionSelectorView>(PagesConstants.MissionSelectorViewPage);
        RegisterViewAsPage<DoMissionView>(PagesConstants.DoMissionViewPage);
        RegisterViewAsPage<FinishUpGameView>(PagesConstants.FinishUpGameViewPage);
        RegisterViewAsPage<LoginView>(PagesConstants.LoginPage);
        RegisterViewAsPage<RegisterView>(PagesConstants.RegisterPage);
        NavigationManagerBase.Current.RegisterPage<MainPage>(PagesConstants.MainPage);
        NavigationManagerBase.Current.RegisterPage<Design.Pages.MainMenuPage>(PagesConstants.MainMenuPage);
        NavigationManagerBase.Current.PushAsync(PagesConstants.LoginPage, true);
    }

    void RegisterViewAsPage<TView>(string name)
        where TView : ContentView, new()
    {
        ((DefaultNavigationManager)NavigationManagerBase.Current).RegisterContentPage<TView>(name);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        NavigationManagerBase.Current.PushAsync(PagesConstants.LoginPage, true);
    }
}

