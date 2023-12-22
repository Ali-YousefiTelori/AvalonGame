using Avalon.GeneratedServices;
using AvalonUI.Helpers;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Navigations;
using System.Collections.ObjectModel;
using System.Text;

namespace AvalonUI.ViewModels;
public class UserProfilesViewModel : PushPageBaseViewModel<bool>
{
    public UserProfilesViewModel()
    {
        OpenItemMenuCommand = new TaskRelayCommand<CheckedProfileContract>(this, OpenItemMenu);
        ChangeSelectedCommand = new RelayCommand<CheckedProfileContract>(ChangeSelected);
        AddNewProfileCommand = new TaskRelayCommand(this, AddNewProfile);
        ContinueCommand = new TaskRelayCommand(this, Continue);
        _ = RefreshList();
    }

    public TaskRelayCommand<CheckedProfileContract> OpenItemMenuCommand { get; set; }
    public RelayCommand<CheckedProfileContract> ChangeSelectedCommand { get; set; }
    public TaskRelayCommand AddNewProfileCommand { get; set; }
    public TaskRelayCommand ContinueCommand { get; set; }
    public ObservableCollection<CheckedProfileContract> Items { get; set; } = new ObservableCollection<CheckedProfileContract>();
    bool _IsProfileSelector;
    public bool IsProfileSelector
    {
        get => _IsProfileSelector;
        set
        {
            _IsProfileSelector = value;
            OnPropertyChanged(nameof(IsProfileSelector));
            OnPropertyChanged(nameof(IsNotProfileSelector));
        }
    }

    public bool IsNotProfileSelector
    {
        get => !_IsProfileSelector;
    }

    private async Task DeleteProfile(CheckedProfileContract contract)
    {
        if (await DisplayQuestion("حذف", $"آیا می‌خواهید '{contract.Profile.Name}' را حذف کنید؟"))
        {
            await ExecuteApi(async () =>
            {
                return await ClientManager.ProfileClient.SoftDeleteByIdAsync(new Int64SoftDeleteRequestContract()
                {
                    Id = contract.Profile.Id,
                    IsDelete = true
                });
            }, RefreshList);
        }
    }

    async Task RefreshList()
    {
        await ExecuteApi<List<ProfileContract>>(async () =>
        {
            return await ClientManager.ProfileClient.FilterAsync(new FilterRequestContract()
            {
            });
        }, (result) =>
        {
            Items.Clear();
            foreach (var item in result.Result)
            {
                Items.Add(new CheckedProfileContract(item));
            }
            return Task.CompletedTask;
        });
    }

    private async Task Continue()
    {
        var selectedItems = Items.Where(x => x.IsSelected).Select(x => x.Profile).ToList();
        if (selectedItems.Count < 5)
            await DisplayAlert("خطا", "برای ادامه‌ی بازی حداقل باید پنج بازیکن را انتخاب کنید،کنار نام هر شخص یک گزینه‌ی انتخاب وجود دارد که می‌توانید با کلیک روی آن بازیکن را انتخاب کنید، اگر بازیکنی وجود ندارد می‌توانید از صفحه‌ی اصلی وارد بخش مدیریت بازیکنان شوید و بازیکنان را اضافه کنید.", "باشه");
        else
        {
            await ExecuteApi<CreateGameResponseContract>(async () =>
            {
                return await ClientManager.GameClient.CreateGameAsync(new CreateGameRequestContract()
                {
                    Profiles = selectedItems.Select(x => x.Id).ToList()
                });
            }, async (result) =>
            {
                await NavigationManagerBase.Current.PushDataAsync((result.Result, selectedItems), PagesConstants.ProfileCardViewerPage);
            });
        }
    }

    private async Task OpenItemMenu(CheckedProfileContract contract)
    {
        var (IsSelected, SelectedItem) = await DisplayActionSheet("عملیات...", "انصراف", null, "حذف");
        if (IsSelected)
        {
            switch (SelectedItem)
            {
                case "حذف":
                    await DeleteProfile(contract);
                    break;
            }
        }
    }

    private void ChangeSelected(CheckedProfileContract contract)
    {
        contract.IsSelected = !contract.IsSelected;
    }

    private async Task AddNewProfile()
    {
        var value = await DisplayPrompt("ثبت پروفایل", "نام مورد نظر را وارد کنید:");
        if (value.HasValue())
        {
            await ExecuteApi(async () =>
            {
                return await ClientManager.ProfileClient.AddAsync(new ProfileBaseContract()
                {
                    Name = value
                });
            }, () =>
            {
                return Task.CompletedTask;
            });
            await RefreshList();
        }
    }

    public override void OnDataInitialized(bool isProfileSelector)
    {
        IsProfileSelector = isProfileSelector;
    }
}

public class CheckedProfileContract : BaseViewModel
{
    public CheckedProfileContract(ProfileContract profile)
    {
        Profile = profile;
    }
    public ProfileContract Profile { get; set; }
    bool _IsSelected;
    public bool IsSelected
    {
        get => _IsSelected;
        set
        {
            _IsSelected = value;
            OnPropertyChanged(nameof(IsSelected));
        }
    }
}