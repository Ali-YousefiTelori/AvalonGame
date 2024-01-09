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
        context.AvalonProfiles.AddRange(
            new AvalonProfileEntity()
            {
                Name = "Ali",
            },
            new AvalonProfileEntity()
            {
                Name = "Yaghob",
            },
            new AvalonProfileEntity()
            {
                Name = "Ebrahim",
            },
            new AvalonProfileEntity()
            {
                Name = "Noah",
            },
            new AvalonProfileEntity()
            {
                Name = "Mosa",
            },
            new AvalonProfileEntity()
            {
                Name = "Younos",
            },
            new AvalonProfileEntity()
            {
                Name = "Yousof",
            },
            new AvalonProfileEntity()
            {
                Name = "Mohammad",
            },
            new AvalonProfileEntity()
            {
                Name = "Sam",
            },
            new AvalonProfileEntity()
            {
                Name = "David",
            },
            new AvalonProfileEntity()
            {
                Name = "Hamid",
            },
            new AvalonProfileEntity()
            {
                Name = "Mahmud",
            },
            new AvalonProfileEntity()
            {
                Name = "Mahdi",
            },
            new AvalonProfileEntity()
            {
                Name = "Hadi",
            },
            new AvalonProfileEntity()
            {
                Name = "Hamed",
            });
        await context.SaveChangesAsync();
        _ = build.RunAsync();
    }
}
