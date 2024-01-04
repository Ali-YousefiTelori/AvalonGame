using AvalonUI.Interfaces;
using System.Web;

namespace AvalonUI.Helpers;
public class GoogleSearchProvider : IGoogleSearch
{
    public async Task OpenSearchResultAsync(string query)
    {
        var encodedQuery = HttpUtility.UrlEncode(query);

        var searchUrl = $"https://www.google.com/search?q={encodedQuery}";

        if (await Launcher.CanOpenAsync(searchUrl))
        {
            await Launcher.OpenAsync(searchUrl);
        }
    }
}