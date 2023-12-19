
using Avalon.Database.Contexts;
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

        static WebApplicationBuilder CreateBuilder(string[] args)
        {
            var app = StartUpExtensions.Create<AvalonContext>(args);
            app.Services.Builder<AvalonContext>();
            app.Services.AddScoped<IUnitOfWork>((serviceProvider) => new AppUnitOfWork(serviceProvider));
            app.Services.AddTransient(serviceProvider => new AvalonContext(serviceProvider.GetService<IEntityFrameworkCoreDatabaseBuilder>()));
            app.Services.AddScoped<IEntityFrameworkCoreDatabaseBuilder, DatabaseBuilder>();

            StartUpExtensions.AddAuthentication("RootAddresses:Authentication");
            StartUpExtensions.AddWhiteLabel("Avalon", "RootAddresses:WhiteLabel");
            return app;
        }
    }
}