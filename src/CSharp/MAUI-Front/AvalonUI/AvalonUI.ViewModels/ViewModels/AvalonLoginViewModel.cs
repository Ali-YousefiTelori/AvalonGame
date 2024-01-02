using EasyMicroservices.UI.Identity.ViewModels.Authentications;
using Identity.GeneratedServices;

namespace AvalonUI.ViewModels;
public class AvalonLoginViewModel : LoginViewModel
{
    public AvalonLoginViewModel(AuthenticationClient authenticationClient) : base(authenticationClient)
    {
    }

    public override Task Load()
    {
        WhiteLabelKey = "5D706148-613A-4C0E-ABB3-ECB41CBD65F6";
        return Task.CompletedTask;
    }
}
