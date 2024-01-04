using Avalon.GeneratedServices;
using AvalonUI.Helpers;
using AvalonUI.Interfaces;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Navigations;
using EasyMicroservices.UI.Identity.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace AvalonUI.ViewModels;
public class MainMenuPageViewModel : PageBaseViewModel
{
    public MainMenuPageViewModel()
    {
        LearnPageCommand = new TaskRelayCommand(LearnPage);
        ProfilesPageCommand = new TaskRelayCommand(ProfilesPage);
        StartCommand = new TaskRelayCommand(Start);
        SendFeedbackCommand = new TaskRelayCommand(SendFeedback);
        LogoutCommand = new TaskRelayCommand(Logout);
        _ = LoadGlobalMessage();
    }

    private async Task ProfilesPage()
    {
        await NavigationManagerBase.Current.PushDataAsync(false, PagesConstants.ProfilesPage);
    }

    private async Task Start()
    {
        await NavigationManagerBase.Current.PushDataAsync(true, PagesConstants.ProfilesPage);
    }
    
    public TaskRelayCommand LearnPageCommand { get; set; }
    public TaskRelayCommand ProfilesPageCommand { get; set; }
    public TaskRelayCommand StartCommand { get; set; }
    public TaskRelayCommand SendFeedbackCommand { get; set; }
    public TaskRelayCommand LogoutCommand { get; set; }

    public string GlobalMessage { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task SendFeedback()
    {
        var message = await DisplayPrompt("پیشنهادات و انتقادات", "لطفا پیشنهاد خود را بنویسید:");
        if (message.HasValue())
        {
            await Send(message);
        }
    }

    async Task Send(string message)
    {
        var result = await ClientManager.UserFeedbackClient.AddAsync(new UserFeedbackContract()
        {
            Message = message
        });
        if (result.IsSuccess)
        {
            await DisplayError("با تشکر از شما، نظر شما را دریافت کردیم.");
        }
        else
        {
            if (await DisplayQuestion("خطا", $"متاسفانه مشکلی پیش آمده است، مجدد برای ارسال تلاش کنیم؟ \r\n {result.Error?.Message}", "بلی", "خیر"))
            {
                await Send(message);
            }
        }
    }
    
    async Task LoadGlobalMessage()
    {
        var result = await ClientManager.UserFeedbackClient.GetGlobalMessageAsync();
        if (result.IsSuccess)
        {
            GlobalMessage = result.Result;
            OnPropertyChanged(nameof(GlobalMessage));
        }
    }

    private async Task Logout()
    {
        if (await DisplayQuestion("خروج", $"آیا می‌خواهید از ناحیه‌ی کاربری خارج شوید؟ در این صورت نام کاربری و رمز را برای ورود باید مجدد وارد کنید.", "بله خارج شو", "خیر"))
        {
            //save data to local storage
            ApplicationDataManager.Current.ApplicationData.UserName = "";
            ApplicationDataManager.Current.ApplicationData.Password = "";
            await ApplicationDataManager.Current.Save();
            for (int i = 0; i < 10; i++)
            {
                await NavigationManagerBase.Current.PopAsync();
            }
            await NavigationManagerBase.Current.PushAsync(PagesConstants.LoginPage);
        }
    }

    private Task LearnPage()
    {
        var serviceSearch = ViewModelLocator.ServiceProvider.GetService<IGoogleSearch>();
        return serviceSearch.OpenSearchResultAsync("آموزش بازی Avalon");
    }
}
