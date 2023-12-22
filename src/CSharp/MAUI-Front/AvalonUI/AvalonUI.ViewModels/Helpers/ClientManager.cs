using Avalon.GeneratedServices;

namespace AvalonUI.Helpers;
public static class ClientManager
{
    static ClientManager()
    {

    }

    const string Url = "http://localhost:6354/";
    public static HttpClient HttpClient { get; } = new HttpClient();
    public static ProfileClient ProfileClient { get; } = new ProfileClient(Url, HttpClient);
    public static GameClient GameClient { get; } = new GameClient(Url, HttpClient);

}
