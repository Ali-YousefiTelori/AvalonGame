using Avalon.GeneratedServices;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using System.Linq;

namespace AvalonUI.ViewModels;
public class DoMissionViewModel : PushPageResponsibleBaseViewModel<List<ProfileContract>, Dictionary<ProfileContract, bool>>
{
    public DoMissionViewModel()
    {
        ShowVoteCommand = new RelayCommand(ShowVote);
        GoNextProfileVoteCommand = new TaskRelayCommand(GoNextProfileVote);
        DoVoteFirstCommand = new TaskRelayCommand(DoVoteFirst);
        DoVoteSecondCommand = new TaskRelayCommand(DoVoteSecond);
    }

    public RelayCommand ShowVoteCommand { get; set; }
    public TaskRelayCommand GoNextProfileVoteCommand { get; set; }
    public TaskRelayCommand DoVoteFirstCommand { get; set; }
    public TaskRelayCommand DoVoteSecondCommand { get; set; }

    public List<ProfileContract> Profiles { get; set; } = new List<ProfileContract>();
    Dictionary<ProfileContract, bool> VotedProfiles { get; set; } = new Dictionary<ProfileContract, bool>();

    public override void OnDataInitialized(List<ProfileContract> profiles)
    {
        Profiles = profiles;
        SetCurrentProfile();
    }

    ProfileContract _CurrentProfile;
    public ProfileContract CurrentProfile
    {
        get => _CurrentProfile;
        set
        {
            _CurrentProfile = value;
            OnPropertyChanged(nameof(CurrentProfile));
        }
    }

    public bool IsFirstFail { get; set; }

    bool _IsShowVote;
    public bool IsShowVote
    {
        get => _IsShowVote;
        set
        {
            _IsShowVote = value;
            OnPropertyChanged(nameof(IsShowVote));
            OnPropertyChanged(nameof(IsNotShowVote));
        }
    }

    public bool IsNotShowVote
    {
        get
        {
            return !IsShowVote;
        }
    }

    private void ShowVote()
    {
        IsShowVote = !IsShowVote;
    }

    private async Task GoNextProfileVote()
    {
        if (await DisplayQuestion("بعدی", "آیا می‌خواهید به مرحله بعدی برویم؟", "بله", "خیر"))
        {
            SetCurrentProfile();
        }
    }
    static Random Random { get; set; } = new Random();
    async void SetCurrentProfile()
    {
        IsShowVote = false;
        CurrentProfile = Profiles.FirstOrDefault(x => !VotedProfiles.ContainsKey(x));
        if (CurrentProfile == null)
            await PopDataAsync(VotedProfiles);
        else
            VotedProfiles.TryAdd(CurrentProfile, false);
        IsFirstFail = Random.Next() % 2 == 0;
        OnPropertyChanged(nameof(IsFirstFail));
    }

    private async Task DoVoteSecond()
    {
        if (!IsFirstFail)
            await QuestionFail();
        else
            await QuestionSuccess();
    }

    private async Task DoVoteFirst()
    {
        if (IsFirstFail)
           await QuestionFail();
        else
            await QuestionSuccess();
    }

    async Task QuestionFail()
    {
        if (await DisplayQuestion("شکست","آیا می‌خواهید ماموریت با شکست روبرو شود؟", "بلی", "خیر"))
        {
            VotedProfiles[CurrentProfile] = false;
            SetCurrentProfile();
        }
    }

    async Task QuestionSuccess()
    {
        if (await DisplayQuestion("موفقیت", "آیا می‌خواهید ماموریت با موفقیت انجام شود؟", "بلی", "خیر"))
        {
            VotedProfiles[CurrentProfile] = true;
            SetCurrentProfile();
        }
    }
}
