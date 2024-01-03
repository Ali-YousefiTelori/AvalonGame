using Avalon.GeneratedServices;
using Identity.GeneratedServices;

namespace AvalonUI.Helpers;
public static class ClientManager
{
    static ClientManager()
    {

    }

    const string Url = "https://avalon.signalgo.ir";//https://avalon.signalgo.ir //"http://localhost:6354"
    const string IdentityUrl = "https://identity.signalgo.ir";
    public static HttpClient HttpClient { get; } = new HttpClient();
    public static ProfileClient ProfileClient { get; } = new ProfileClient(Url, HttpClient);
    public static GameClient GameClient { get; } = new GameClient(Url, HttpClient);
    public static AuthenticationClient AuthenticationClient { get; } = new AuthenticationClient(IdentityUrl, HttpClient);

}
