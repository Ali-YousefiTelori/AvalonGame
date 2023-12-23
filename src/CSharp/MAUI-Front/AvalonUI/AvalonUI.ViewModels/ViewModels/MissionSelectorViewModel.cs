using Avalon.GeneratedServices;
using AvalonUI.Helpers;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Navigations;

namespace AvalonUI.ViewModels;
public class MissionSelectorViewModel : PushPageBaseViewModel<(CreateGameResponseContract Game, List<ProfileContract> Profiles)>
{
    public MissionSelectorViewModel()
    {
        CrownChangeCommand = new RelayCommand(CrownChange);
        GoToMissionCommand = new TaskRelayCommand(this, GoToMission);
        ChangeSelectedCommand = new RelayCommand<CheckedProfileContract>(ChangeSelected);
        CloseMissionResultCommand = new TaskRelayCommand(CloseMissionResult);
        ShowGameResultCommand = new RelayCommand<string>(ShowGameResult);
    }

    async Task CloseMissionResult()
    {
        IsShowMissionResult = false;
        if (GameMissionsResults.Count(x => x.GameMission.IsFailed.Value) > 2 || GameMissionsResults.Count(x => !x.GameMission.IsFailed.Value) > 2)
            await NavigationManagerBase.Current.PushDataAsync((Game, Profiles.Select(x => x.Profile).ToList(), GameMissionsResults), PagesConstants.FinishUpGameViewPage);
    }

    public RelayCommand CrownChangeCommand { get; set; }
    public TaskRelayCommand CloseMissionResultCommand { get; set; }
    public RelayCommand<string> ShowGameResultCommand { get; set; }

    public RelayCommand<CheckedProfileContract> ChangeSelectedCommand { get; set; }
    public TaskRelayCommand GoToMissionCommand { get; set; }
    public CreateGameResponseContract Game { get; set; }
    public List<CheckedProfileContract> Profiles { get; set; }
    public List<ProfileContract> SelectedCrownProfiles { get; set; } = new List<ProfileContract>();
    public List<OfflineGameMissionContract> GameMissions { get; set; }
    public List<GameMissionResult> GameMissionsResults { get; set; } = new List<GameMissionResult>();
    public override void OnDataInitialized((CreateGameResponseContract Game, List<ProfileContract> Profiles) data)
    {
        Game = data.Game;
        Profiles = data.Profiles
                .OrderBy(x => Guid.NewGuid())
                .Select(x => new CheckedProfileContract(x))
                .ToList();
        OnPropertyChanged(nameof(Profiles));
        _ = FetchMissions();
    }

    public bool? IsMissionOneFailed { get; set; }
    public bool? IsMissionTwoFailed { get; set; }
    public bool? IsMissionThreeFailed { get; set; }
    public bool? IsMissionFourFailed { get; set; }
    public bool? IsMissionFiveFailed { get; set; }

    int _CurrentMissionNumber;
    public int CurrentMissionNumber
    {
        get => _CurrentMissionNumber;
        set
        {
            _CurrentMissionNumber = value;
            OnPropertyChanged(nameof(CurrentMissionNumber));
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

    int NumberOfFailsNeed { get; set; }
    string SelectorProfileName { get; set; }
    int CrownChangedCount { get; set; }
    int PlayersCount { get; set; }

    bool _IsShowMissionResult;
    public bool IsShowMissionResult
    {
        get => _IsShowMissionResult;
        set
        {
            _IsShowMissionResult = value;
            OnPropertyChanged(nameof(IsShowMissionResult));
            OnPropertyChanged(nameof(IsNotShowMissionResult));
        }
    }

    public bool IsNotShowMissionResult
    {
        get
        {
            return !IsShowMissionResult;
        }
    }


    GameMissionResult _CurrentGameMissionResult;
    public GameMissionResult CurrentGameMissionResult
    {
        get => _CurrentGameMissionResult;
        set
        {
            _CurrentGameMissionResult = value;
            OnPropertyChanged(nameof(CurrentGameMissionResult));
        }
    }

    OfflineGameMissionContract GetCurrentMission(int missionNumber)
    {
        var currentMission = GameMissions.FirstOrDefault(x => x.Index == missionNumber);
        return currentMission;
    }

    public void SetMission(int missionNumber)
    {
        CrownChangedCount = 0;
        CurrentMissionNumber = missionNumber;
        var currentMission = GetCurrentMission(missionNumber);
        if (currentMission != null)
        {
            NumberOfFailsNeed = currentMission.DoNeedsTwoOfFails ? 2 : 1;
            PlayersCount = currentMission.PlayerCount;
        }
        CrownChange();
    }

    void RefreshDescription()
    {
        GameDescription = $"شما در ماموریت {CurrentMissionNumber} هستید، برای اینکه این ماموریت با شکست مواجه شود، رای {NumberOfFailsNeed} مافیا کافیست. شهروندان تلاش کنند در رای‌گیری مافیا با خود به ماموریت نبرند، بعد از اتمام انتخاب‌های خود روی گزینه‌ی ماموریت کلیک کنید و وارد ماموریت شوید، تاج دست {SelectorProfileName} هست و او انتخاب می‌کند که چه کسانی به ماموریت بروند، تا به الان {CrownChangedCount} تاج از دست داده‌اید، اگر تعداد تاج‌های از دست رفته به عدد 6 برسد، شهروندان بازی را می‌بازند. برای انجام این ماموریت {PlayersCount} شخص کافی می‌باشد";
    }

    async Task FetchMissions()
    {
        await ExecuteApi<List<OfflineGameMissionContract>>(async () =>
        {
            return await ClientManager.GameClient.GetGameMissionsAsync(new Int64GetByIdRequestContract()
            {
                Id = Game.GameId
            });
        }, (result) =>
        {
            GameMissions = result.Result;
            SetMission(1);
            return Task.CompletedTask;
        }, async ex =>
        {
            await DisplayAlert("خطا", $"خطا در دریافت اطلاعات رخ داده است، بدون دریافت اطلاعات نمی‌توانیم به بازی ادامه دهیم، باشه را بزنید تا مجدد تلاش کنیم. جزئیات : {ex.Message}", "باشه");
            await FetchMissions();
        });
    }

    private void ChangeSelected(CheckedProfileContract contract)
    {
        contract.IsSelected = !contract.IsSelected;
    }

    private void CrownChange()
    {
        var selectProfile = () => Profiles
                .FirstOrDefault(x => !SelectedCrownProfiles.Contains(x.Profile));
        var profile = selectProfile();
        if (profile == null)
        {
            SelectedCrownProfiles.Clear();
            profile = selectProfile();
        }
        SelectedCrownProfiles.Add(profile?.Profile);
        SelectorProfileName = profile?.Profile?.Name;
        CrownChangedCount++;
        RefreshDescription();
    }

    private async Task GoToMission()
    {
        var selectedProfiles = Profiles.Where(x => x.IsSelected).ToList();
        if (selectedProfiles.Count != PlayersCount)
        {
            await DisplayAlert("انتخاب اشخاص", $"تعداد افرادی که باید به ماموریت بروند {PlayersCount} نفر می‌باشند، اما شما {selectedProfiles.Count} نفر را انتخاب کرده‌اید، لطفا از لیست اشخاص کسانی که می‌خواهید به ماموریت بروند را به تعدادی که لازم است انتخاب کنید.", "باشه");
            return;
        }

        if (await DisplayQuestion("رفتن به ماموریت", $"آیا میخواهید {string.Join(" و ", selectedProfiles.Select(x => x.Profile.Name))} به ماموریت بروند؟", "بلی", "خیر"))
        {
            var result = await NavigationManagerBase.Current.PushDataAsync<List<ProfileContract>, Dictionary<ProfileContract, bool>>(selectedProfiles.Select(x => x.Profile).ToList(), PagesConstants.DoMissionViewPage);
            if (result != null && result.Count == PlayersCount)
            {
                bool isMissionFailed = result.Values.Count(x => !x) >= NumberOfFailsNeed;
                if (CurrentMissionNumber == 1)
                    IsMissionOneFailed = isMissionFailed;
                else if (CurrentMissionNumber == 2)
                    IsMissionTwoFailed = isMissionFailed;
                else if (CurrentMissionNumber == 3)
                    IsMissionThreeFailed = isMissionFailed;
                else if (CurrentMissionNumber == 4)
                    IsMissionFourFailed = isMissionFailed;
                else if (CurrentMissionNumber == 5)
                    IsMissionFiveFailed = isMissionFailed;

                OnPropertyChanged(nameof(IsMissionOneFailed));
                OnPropertyChanged(nameof(IsMissionTwoFailed));
                OnPropertyChanged(nameof(IsMissionThreeFailed));
                OnPropertyChanged(nameof(IsMissionFourFailed));
                OnPropertyChanged(nameof(IsMissionFiveFailed));

                await SetMissionResult(result.Values.Count(x => !x), isMissionFailed, result.Select(x => x.Key.Name).ToArray());
            }
        }
    }

    async Task SetMissionResult(int failCount, bool isMissionFailed, string[] profiles)
    {
        var currentMission = GetCurrentMission(CurrentMissionNumber);
        currentMission.IsFailed = isMissionFailed;
        currentMission.FailCount = failCount;
        string result = "";
        if (isMissionFailed)
            result = $"شکست مواجه شد. تعداد کسانی که ماموریت را با شکست مواجه کردند {failCount} نفر بودند، ";
        else
            result = "موفقیت انجام شد.";
        string description = $"ماموریت با {result} کسانی که در ماموریت بودند {string.Join(" و ", profiles)} بودند.";
        var gameMissionResult = new GameMissionResult()
        {
            GameMission = currentMission,
            Description = description
        };
        GameMissionsResults.Add(gameMissionResult);
        CurrentGameMissionResult = gameMissionResult;
        await ExecuteApi(async () =>
        {
            return await ClientManager.GameClient.CreateMissionResultAsync(new CreateGameMissionRequestContract()
            {
                GameMissionId = currentMission.Id,
                FailCount = failCount,
            });
        }, () =>
        {
            IsShowMissionResult = true;
            if (CurrentMissionNumber < 5)
                SetMission(CurrentMissionNumber + 1);
            return Task.CompletedTask;
        }, async ex =>
        {
            await DisplayAlert("خطا", $"خطا در دریافت اطلاعات رخ داده است، بدون دریافت اطلاعات نمی‌توانیم به بازی ادامه دهیم، باشه را بزنید تا مجدد تلاش کنیم. جزئیات : {ex.Message}", "باشه");
            await SetMissionResult(failCount, isMissionFailed, profiles);
        });
    }

    private void ShowGameResult(string number)
    {
        var currentMission = GetCurrentMission(int.Parse(number));
        if (!currentMission.IsFailed.HasValue)
            return;
        CurrentGameMissionResult = GameMissionsResults.FirstOrDefault(x => x.GameMission.Id == currentMission.Id);
        IsShowMissionResult = true;
    }
}

public class GameMissionResult
{
    public OfflineGameMissionContract GameMission { get; set; }
    public string Description { get; set; }
}
