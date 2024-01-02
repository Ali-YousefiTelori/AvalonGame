using Avalon.Database.Contexts;
using Avalon.Database.Entities;
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
        using var scope = ServiceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetService<AvalonContext>();
        context.Profiles.AddRange(
            new ProfileEntity()
            {
                Name = "Ali",
            },
            new ProfileEntity()
            {
                Name = "Yaghob",
            },
            new ProfileEntity()
            {
                Name = "Ebrahim",
            },
            new ProfileEntity()
            {
                Name = "Noah",
            },
            new ProfileEntity()
            {
                Name = "Mosa",
            },
            new ProfileEntity()
            {
                Name = "Younos",
            },
            new ProfileEntity()
            {
                Name = "Yousof",
            },
            new ProfileEntity()
            {
                Name = "Mohammad",
            },
            new ProfileEntity()
            {
                Name = "Sam",
            },
            new ProfileEntity()
            {
                Name = "David",
            },
            new ProfileEntity()
            {
                Name = "Hamid",
            },
            new ProfileEntity()
            {
                Name = "Mahmud",
            },
            new ProfileEntity()
            {
                Name = "Mahdi",
            },
            new ProfileEntity()
            {
                Name = "Hadi",
            },
            new ProfileEntity()
            {
                Name = "Hamed",
            });
        await context.SaveChangesAsync();
        _ = build.RunAsync();
    }
}
