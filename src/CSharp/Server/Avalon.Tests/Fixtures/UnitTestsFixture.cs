using Avalon.Database.Contexts;
using Avalon.WebApp;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Avalon.Tests.Fixtures;
public class UnitTestsFixture : IAsyncLifetime
{
    public IServiceProvider ServiceProvider { get; private set; }
    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        var app = Program.CreateBuilder(null);
        //app.Services.AddControllers().AddApplicationPart(typeof(UserController).Assembly);
        app.WebHost.UseUrls($"http://localhost:{8280}");
        var build = await app.Build<AvalonContext>(true);
        build.MapControllers();
        ServiceProvider = app.Services.BuildServiceProvider();
        _ = build.RunAsync();
    }
}
