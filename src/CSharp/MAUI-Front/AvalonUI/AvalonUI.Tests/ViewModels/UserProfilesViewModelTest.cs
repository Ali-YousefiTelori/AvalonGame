using AvalonUI.ViewModels;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Navigations;
using EasyMicroservices.UI.MauiComponents.Navigations;
using System.Text;

namespace AvalonUI.Tests.ViewModels;
public class UserProfilesViewModelTest
{
    public UserProfilesViewModelTest()
    {
        NavigationManagerBase.Current = new TestNavigation();
    }

    TestUserProfilesViewModel GetViewModelInstance()
    {
        var result = new TestUserProfilesViewModel();
        ContinueViewModel(result);
        return result;
    }

    void ContinueViewModel(BaseViewModel baseViewModel)
    {
        while (baseViewModel.IsBusy)
            continue;
    }

    [Fact]
    public async Task RefreshList()
    {
        var vm = GetViewModelInstance();
        await vm.RefreshList();
        Assert.True(vm.Items.Count > 0);
        Assert.True(vm.Items.All(x => x.Profile != null));
        Assert.True(vm.Items.All(x => x.Profile.Name.HasValue()));
        Assert.True(vm.Items.All(x => x.Profile.Id > 0));
    }

    [Fact]
    public async Task OpenItemMenuCommandDelete()
    {
        var vm = GetViewModelInstance();
        var item = vm.Items.First();
        await vm.OpenItemMenuCommand.ExecuteAsync(item);
        Assert.True(!vm.Items.Any(x => x == item));
        Assert.True(vm.Items.Count > 0);
    }

    [Fact]
    public void ChangeSelectedCommand()
    {
        var vm = GetViewModelInstance();
        var item = vm.Items.First();
        Assert.True(!item.IsSelected);
        vm.ChangeSelectedCommand.Execute(item);
        Assert.True(item.IsSelected);
        vm.ChangeSelectedCommand.Execute(item);
        Assert.True(!item.IsSelected);
    }

    [Fact]
    public async Task AddNewProfileCommand()
    {
        var vm = GetViewModelInstance();
        await vm.AddNewProfileCommand.ExecuteAsync(null);
        Assert.Contains(vm.Items, x => x.Profile.Name == "AddedNew");
        Assert.True(vm.Items.Count > 0);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(2, false)]
    [InlineData(3, false)]
    [InlineData(4, false)]
    [InlineData(5, true)]
    [InlineData(6, true)]
    [InlineData(7, true)]
    [InlineData(8, true)]
    [InlineData(9, true)]
    [InlineData(10, true)]
    [InlineData(11, true)]
    [InlineData(12, true)]
    [InlineData(13, true)]
    [InlineData(14, true)]
    public async Task ContinueCommand(int profileCount, bool isDone)
    {
        var vm = GetViewModelInstance();
        for (int i = 0; i < profileCount; i++)
        {
            vm.Items[i].IsSelected = true;
        }
        await vm.ContinueCommand.ExecuteAsync(null);
        if (isDone)
            Assert.Null(vm.DialogShows);
        else
            Assert.Equal("DisplayAlert", vm.DialogShows);
    }
}

public class TestUserProfilesViewModel : UserProfilesViewModel
{
    public string DialogShows { get; set; }
    public override Task DisplayError(string message)
    {
        DialogShows = nameof(DisplayError);
        throw new Exception(message);
    }

    public override async Task<(bool IsSelected, string SelectedItem)> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
    {
        DialogShows = nameof(DisplayActionSheet);
        return (true, buttons.First());
    }

    public override Task<bool> DisplayQuestion(string title, string message, string accept = "Yes", string cancel = "No")
    {
        DialogShows = nameof(DisplayQuestion);
        return Task.FromResult(true);
    }

    public override Task<string> DisplayPrompt(string title, string message)
    {
        DialogShows = nameof(DisplayPrompt);
        return Task.FromResult("AddedNew");
    }

    public override Task DisplayAlert(string title, string message, string cancel)
    {
        DialogShows = nameof(DisplayAlert);
        return Task.CompletedTask;
    }
}

public class TestNavigation : NavigationManagerBase
{
    public override async Task<bool> OpenBrowser(string url)
    {
        return default;
    }

    public override async Task<(bool IsSusccess, Stream Stream, string FileName, string ContentType)> PickFile()
    {
        return default;
    }

    public override async Task PopAsync()
    {
    }

    public override async Task<TResponseData> PushAsync<TResponseData>(string pageName, bool doClear = false)
    {
        return default;
    }

    public override async Task PushAsync(string pageName, bool doClear = false)
    {
    }

    public override async Task<TResponseData> PushDataAsync<TData, TResponseData>(TData data, string pageName, bool doClear = false)
    {
        return default;
    }

    public override async Task PushDataAsync<TData>(TData data, string pageName, bool doClear = false)
    {
    }
}