using AvalonUI.Design.Views;
using AvalonUI.Helpers;
using EasyMicroservices.UI.Cores.Interfaces;
using EasyMicroservices.UI.Cores.Navigations;
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
        NavigationManagerBase.Current.RegisterPage<MainPage>(PagesConstants.MainPage);
        NavigationManagerBase.Current.RegisterPage<Design.Pages.MainMenuPage>(PagesConstants.MainMenuPage);
    }

    void RegisterViewAsPage<TView>(string name)
        where TView : ContentView, new()
    {
        ((DefaultNavigationManager)NavigationManagerBase.Current).RegisterContentPage<TView>(name);
    }
}

