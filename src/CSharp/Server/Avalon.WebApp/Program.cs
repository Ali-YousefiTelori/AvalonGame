using Avalon.Database.Contexts;
using Avalon.Logics;
using Avalon.Models;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;

namespace Avalon.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var app = CreateBuilder(args);
            var build = await app.BuildWithUseCors<AvalonContext>(null, true);
            build.MapControllers();
            build.Run();
        }

        public static WebApplicationBuilder CreateBuilder(string[] args)
        {
            var app = StartUpExtensions.Create<AvalonContext>(args);
            app.Services.Builder<AvalonContext>().UseDefaultSwaggerOptions();
            app.Services.AddScoped<IUnitOfWork>((serviceProvider) => new AppUnitOfWork(serviceProvider));
            app.Services.AddScoped((serviceProvider) => new AppUnitOfWork(serviceProvider));
            app.Services.AddTransient(serviceProvider => new AvalonContext(serviceProvider.GetService<IEntityFrameworkCoreDatabaseBuilder>()));
            app.Services.AddSingleton<IEntityFrameworkCoreDatabaseBuilder, DatabaseBuilder>();
            app.Services.AddScoped<GameCreatorLogic>();
            app.Services.AddScoped<GameMissionsLogic>();

            StartUpExtensions.AddAuthentication("RootAddresses:Authentication");
            StartUpExtensions.AddWhiteLabel("Avalon", "RootAddresses:WhiteLabel");
            return app;
        }
    }
}