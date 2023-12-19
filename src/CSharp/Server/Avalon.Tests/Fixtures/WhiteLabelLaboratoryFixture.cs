using Avalon.Database.Contexts;
using Avalon.WebApp;
using EasyMicroservices.WhiteLabelsMicroservice.VirtualServerForTests;
using EasyMicroservices.WhiteLabelsMicroservice.VirtualServerForTests.TestResources;
using Microsoft.Extensions.Configuration;

namespace Avalon.Tests.Fixtures;
public class WhiteLabelLaboratoryFixture : IAsyncLifetime
{
    public const string localhost = "127.0.0.1";
    protected int Port = 1041;
    protected static HttpClient HttpClient { get; set; } = new HttpClient();
    public WhiteLabelLaboratoryFixture()
    {

    }
    public async Task InitializeAsync()
    {
        var whiteLabelVirtualTestManager = new WhiteLabelVirtualTestManager();

        if (await whiteLabelVirtualTestManager.OnInitialize(Port))
        {
            Console.WriteLine($"WhiteLabelVirtualTestManager Initialized! {Port}");
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            foreach (var item in WhiteLabelResource.GetResources(new AvalonContext(new DatabaseBuilder(config)), "Avalon"))
            {
                whiteLabelVirtualTestManager.AppendService(Port, item.Key, item.Value);
            }
        }
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}