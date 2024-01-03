using EasyMicroservices.UI.MauiComponents.Design.Pages;

namespace AvalonUI.Design.Pages;

public partial class MainMenuPage : EasyContentPage
{
	public MainMenuPage()
	{
		InitializeComponent();
	}

    bool canExit = false;
    protected override bool OnBackButtonPressed()
    {
        if (canExit)
            return base.OnBackButtonPressed();
        Task.Run(async () =>
        {
            await Dispatcher.DispatchAsync(async () =>
            {
                if (await DisplayAlert("خروج", "آیا می‌خواهید خارج شوید؟", "بلی", "خیر"))
                {
                    canExit = true;
                    Application.Current.Quit();
                }
            });
        });
        return true;
    }
}