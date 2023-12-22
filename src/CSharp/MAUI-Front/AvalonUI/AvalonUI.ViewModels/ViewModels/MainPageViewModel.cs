using Avalon.GeneratedServices;
using AvalonUI.Helpers;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Navigations;

namespace AvalonUI.ViewModels;
public class MainPageViewModel : BaseViewModel
{
    public MainPageViewModel()
    {
        ProfilesPageCommand = new TaskRelayCommand(ProfilesPage);
        StartCommand = new TaskRelayCommand(Start);
    }

    private async Task ProfilesPage()
    {
        await NavigationManagerBase.Current.PushDataAsync(false, PagesConstants.ProfilesPage);
    }

    private async Task Start()
    {
        await NavigationManagerBase.Current.PushDataAsync(true, PagesConstants.ProfilesPage);
    }

    public TaskRelayCommand ProfilesPageCommand { get; set; }
    public TaskRelayCommand StartCommand { get; set; }
}
