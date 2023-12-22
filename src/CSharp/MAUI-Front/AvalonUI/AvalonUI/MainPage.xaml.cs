using AvalonUI.Design.Views;
using AvalonUI.Helpers;
using EasyMicroservices.UI.Cores.Interfaces;
using EasyMicroservices.UI.Cores.Navigations;
using EasyMicroservices.UI.MauiComponents.Navigations;

namespace AvalonUI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        NavigationManagerBase.Current = new DefaultNavigationManager(Navigation);
        RegisterViewAsPage<UserProfilesView>(PagesConstants.ProfilesPage);
        RegisterViewAsPage<ProfileCardViewer>(PagesConstants.ProfileCardViewerPage);
        RegisterViewAsPage<MissionSelectorView>(PagesConstants.MissionSelectorViewPage);
        RegisterViewAsPage<DoMissionView>(PagesConstants.DoMissionViewPage);
    }

    void RegisterViewAsPage<TView>(string name)
        where TView : ContentView, new()
    {
        ((DefaultNavigationManager)NavigationManagerBase.Current).RegisterContentPage<TView>(name);
    }
}

