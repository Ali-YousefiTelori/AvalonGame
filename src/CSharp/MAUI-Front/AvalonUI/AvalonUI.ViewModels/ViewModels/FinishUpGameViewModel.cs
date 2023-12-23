using Avalon.GeneratedServices;
using AvalonUI.Helpers;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Navigations;
using System.Text;

namespace AvalonUI.ViewModels;
public class FinishUpGameViewModel : PushPageBaseViewModel<(CreateGameResponseContract Game, List<ProfileContract> Profiles, List<GameMissionResult> MissionResults)>
{
    public FinishUpGameViewModel()
    {
        SelectMerlinCommand = new TaskRelayCommand<ProfileDetails>(SelectMerlin);
        ResetGameCommand = new TaskRelayCommand(ResetGame);
    }

    public TaskRelayCommand<ProfileDetails> SelectMerlinCommand { get; set; }
    public TaskRelayCommand ResetGameCommand { get; set; }

    public CreateGameResponseContract Game { get; set; }
    public List<ProfileDetails> Profiles { get; set; }
    public List<GameMissionResult> MissionResults { get; set; }
    bool? _IsMerlinWins;
    public bool? IsMerlinWins
    {
        get => _IsMerlinWins;
        set
        {
            _IsMerlinWins = value;
            OnPropertyChanged(nameof(IsMerlinWins));
            OnPropertyChanged(nameof(IsShowGameResult));
            OnPropertyChanged(nameof(IsNotShowGameResult));
        }
    }

    public bool IsShowGameResult
    {
        get
        {
            return IsMerlinWins.HasValue;
        }
    }

    public bool IsNotShowGameResult
    {
        get
        {
            return !IsShowGameResult;
        }
    }

    string _GameDescription;
    public string GameDescription
    {
        get => _GameDescription;
        set
        {
            _GameDescription = value;
            OnPropertyChanged(nameof(GameDescription));
        }
    }

    public override void OnDataInitialized((CreateGameResponseContract Game, List<ProfileContract> Profiles, List<GameMissionResult> MissionResults) data)
    {
        MissionResults = data.MissionResults;
        Game = data.Game;
        Profiles = data.Profiles.Select(x => new ProfileDetails()
        {
            Profile = x,
            IsMinionOfMordred = Game.GameProfiles.FirstOrDefault(g => g.ProfileId == x.Id).IsMinionOfMordred
        }).OrderByDescending(x => !x.IsMinionOfMordred).ToList();
        OnPropertyChanged(nameof(Profiles));
    }

    private async Task SelectMerlin(ProfileDetails profileDetails)
    {
        if (profileDetails.IsMinionOfMordred)
        {
            await DisplayAlert("خطا", "شما نمی‌توانید یک مافیا را به عنوان مرلین انتخاب کنید. لطفا از شهروندان انتخاب کنید.", "باشه");
        }
        else
        {
            if (await DisplayQuestion("انتخاب مرلین", $"آیا می‌خواهید {profileDetails.Profile.Name} را به عنوان مرلین انتخاب کنید؟", "بلی", "خیر"))
            {
                var gameProfile = Game.GameProfiles.FirstOrDefault(x => x.ProfileId == profileDetails.Profile.Id);
                StringBuilder builder = new StringBuilder();
                bool isMerlinWin = false;
                var failedMissions = MissionResults.Count(x => x.GameMission.IsFailed.GetValueOrDefault());
                if (gameProfile.RoleName == "Merlin")
                {
                    if (failedMissions > 2)
                    {
                        builder.AppendLine("مافیا بازی را پیروز شده بودند، با اینحال مرلین را هم حدس زدند. پیروز بازی تیم مافیاست.");
                    }
                    else
                    {
                        builder.AppendLine("مافیا مرلین را درست تشخیص دادند و پیروز شدند.");
                    }
                }
                else
                {
                    if (failedMissions > 2)
                    {
                        builder.AppendLine($"مافیا مرلین را درست تشخیص ندادند آنها {profileDetails.Profile.Name} را انتخاب کردند، اما چون تعداد ماموریت‌هایی که در آن موفق شدند از شهروندان بیشتر بود مافیا پیروز شدند.");
                    }
                    else
                    {
                        isMerlinWin = true;
                        builder.AppendLine($"مافیا مرلین را درست تشخیص ندادند آنها {profileDetails.Profile.Name} را انتخاب کردند، شهروندان پیروز شدند.");
                    }
                }

                builder.AppendLine("نقش‌ها:");
                var profileRoles = Game.GameProfiles.Select(x => new
                {
                    ProfileName = Profiles.FirstOrDefault(p => p.Profile.Id == x.ProfileId).FullName,
                    RoleName = x.RoleName,
                });
                builder.Append(string.Join(Environment.NewLine, profileRoles.Select(x => $"{x.ProfileName}:{x.RoleName}")));
                GameDescription = builder.ToString();
                IsMerlinWins = isMerlinWin;
                await FinishUpGame(profileDetails);
            }
        }
    }

    async Task FinishUpGame(ProfileDetails profileDetails)
    {
        await ExecuteApi(async () =>
        {
            return await ClientManager.GameClient.FinishUpGameAsync(new FinishGameRequestContract()
            {
                GameId = Game.GameId,
                GuessMerlinProfileId = profileDetails.Profile.Id
            });
        }, () => Task.CompletedTask, async (ex) =>
        {
            await DisplayAlert("خطا", $"خطا در دریافت اطلاعات رخ داده است، بدون دریافت اطلاعات نمی‌توانیم به بازی ادامه دهیم، باشه را بزنید تا مجدد تلاش کنیم. جزئیات : {ex.Message}", "باشه");
            await FinishUpGame(profileDetails);
        });
    }
    private async Task ResetGame()
    {
        if (await DisplayQuestion("اتمام", "آیا می‌خواهید به صفحه اصلی بازگردیم؟", "بلی", "خیر"))
        {
            for (int i = 0; i < 10; i++)
            {
                await NavigationManagerBase.Current.PopAsync();
            }
        }
    }
}

public class ProfileDetails
{
    public ProfileContract Profile { get; set; }
    public string FullName
    {
        get
        {
            return $"{Profile.Name} ({(IsMinionOfMordred ? "مافیا" : "شهروند")})";
        }
    }

    public bool IsMinionOfMordred { get; set; }
}
