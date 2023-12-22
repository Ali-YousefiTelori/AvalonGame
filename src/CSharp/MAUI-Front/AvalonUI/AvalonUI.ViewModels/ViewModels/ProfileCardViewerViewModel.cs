using Avalon.GeneratedServices;
using AvalonUI.Helpers;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Navigations;

namespace AvalonUI.ViewModels;
public class ProfileCardViewerViewModel : PushPageBaseViewModel<(CreateGameResponseContract Game, List<ProfileContract> Profiles)>
{
    public ProfileCardViewerViewModel()
    {
        ShowRoleCommand = new TaskRelayCommand(ShowRole);
        GoNextProfileCommand = new TaskRelayCommand(GoNextProfile);
    }

    public TaskRelayCommand ShowRoleCommand { get; set; }
    public TaskRelayCommand GoNextProfileCommand { get; set; }

    CreateGameResponseContract CreateGameResponseContract { get; set; }
    List<ProfileContract> Profiles { get; set; }
    List<ProfileContract> ShowedProfiles { get; set; } = new List<ProfileContract>();
    public override void OnDataInitialized((CreateGameResponseContract Game, List<ProfileContract> Profiles) data)
    {
        Profiles = data.Profiles;
        CreateGameResponseContract = data.Game;
        SetProfile(data.Game.GameProfiles.First().ProfileId);
    }

    AppGameProfileContract _CurrentProfile;
    public AppGameProfileContract CurrentProfile
    {
        get => _CurrentProfile;
        set
        {
            _CurrentProfile = value;
            OnPropertyChanged(nameof(CurrentProfile));
        }
    }

    public bool IsNotShowRole
    {
        get => !IsShowRole;
    }

    bool _IsShowRole;
    public bool IsShowRole
    {
        get => _IsShowRole;
        set
        {
            _IsShowRole = value;
            OnPropertyChanged(nameof(IsShowRole));
            OnPropertyChanged(nameof(IsNotShowRole));
        }
    }

    string _ProfileRoleImage;
    public string ProfileRoleImage
    {
        get => _ProfileRoleImage;
        set
        {
            _ProfileRoleImage = value;
            OnPropertyChanged(nameof(ProfileRoleImage));
        }
    }


    string _RoleDescription;
    public string RoleDescription
    {
        get => _RoleDescription;
        set
        {
            _RoleDescription = value;
            OnPropertyChanged(nameof(RoleDescription));
        }
    }

    void SetProfile(long profileId)
    {
        CurrentProfile = new AppGameProfileContract()
        {
            Game = CreateGameResponseContract.GameProfiles.FirstOrDefault(x => x.ProfileId == profileId),
            Profile = Profiles.FirstOrDefault(x => x.Id == profileId)
        };
        ShowedProfiles.Add(CurrentProfile.Profile);
        ProfileRoleImage = CurrentProfile.Game.RoleName.ToLower() + ".png";
        if (CurrentProfile.Game.IsMinionOfMordred)
            RoleDescription = GetMafiaRoleDescription(CurrentProfile.Game.RoleName);
        else
            RoleDescription = GetPeopleRoleDescription(CurrentProfile.Game.RoleName);
    }

    string GetMafiaRoleDescription(string roleName)
    {
        if (roleName == "Mordred")
            return "شما 'موردرد' سردسته‌ی مافیا هستید و توسط 'مرلین' دیده نمی‌شوید و باید شهروندان را گمراه کرده و مرلین را شناسایی کنید";
        else if (roleName == "Morgana")
            return "شما 'مورگانا' یک مافیا هستید و باید نقش 'مرلین' را برای 'پرسیوال' بازی کنید و باید شهروندان را گمراه کرده و 'مرلین' را شناسایی کنید";
        else if (roleName == "Oberon")
            return "شما 'اوبرون' یک مافیا هستید و دوستان مافیای خود را نمی‌شناسید و آنها هم شمارا نمی‌شناسند و باید شهروندان را گمراه کرده و 'مرلین' را شناسایی کنید";
        return "شما یک مافیا هستید و باید شهروندان را گمراه کرده و 'مرلین' را شناسایی کنید";
    }

    string GetPeopleRoleDescription(string roleName)
    {
        if (roleName == "Merlin")
            return "شما 'مرلین' سردسته‌ی شهروندان هستید و باید تلاش کنید که شهروندان را نجات دهید و نقش شما نباید توسط مافیا حین بازی و حتی در انتهای بازی شناسایی شود.";
        else if (roleName == "Percival")
            return "شما 'پرسیوال' یک شهروند خوب هستید و مرلین را می‌شناسید و باید از مرلین کمک گرفته و مافیا را شناسایی کرده و از مرلین محافظت کنید. توجه داشته باشید که ممکن است 'مورگانا' که یک مافیا است شما را گمراه کند";
        return "شما یک شهروند خوب هستید و باید مافیا را شناسایی کرده و از مرلین محافظت کنید";
    }

    private async Task ShowRole()
    {
        if (await DisplayQuestion("نقش", "آیا می‌خواهید نقش شما نمایش داده شود؟", "بلی", "خیر"))
            IsShowRole = true;
    }

    private async Task GoNextProfile()
    {
        if (await DisplayQuestion("بعدی", "آیا می‌خواهید سراغ بازیکن بعدی برویم؟", "بلی", "خیر"))
        {
            IsShowRole = false;
            var nextProfile = Profiles.FirstOrDefault(x => !ShowedProfiles.Contains(x));
            if (nextProfile != null)
                SetProfile(nextProfile.Id);
            else
            {
                await NavigationManagerBase.Current.PushDataAsync((CreateGameResponseContract, Profiles), PagesConstants.MissionSelectorViewPage);
            }
        }
    }
}

public class AppGameProfileContract
{
    public GameProfileContract Game { get; set; }
    public ProfileContract Profile { get; set; }
}
