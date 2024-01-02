using Avalon.Database.Contexts;
using Avalon.Logics;
using Avalon.Models;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

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
            app.Services.Builder<AvalonContext>("Avalon").UseDefaultSwaggerOptions();
            app.Services.AddScoped<IUnitOfWork>((serviceProvider) => new AppUnitOfWork(serviceProvider));
            app.Services.AddScoped((serviceProvider) => new AppUnitOfWork(serviceProvider));
            app.Services.AddTransient(serviceProvider => new AvalonContext(serviceProvider.GetService<IEntityFrameworkCoreDatabaseBuilder>()));
            app.Services.AddSingleton<IEntityFrameworkCoreDatabaseBuilder, DatabaseBuilder>();
            app.Services.AddScoped<GameCreatorLogic>();
            app.Services.AddScoped<GameMissionsLogic>();
            //app.Services.AddRateLimiter(_ => _
            //    .AddFixedWindowLimiter(policyName: "fixed", options =>
            //    {
            //        options.PermitLimit = 15;
            //        options.Window = TimeSpan.FromSeconds(30);
            //        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            //        options.QueueLimit = 5;
            //    }));
            return app;
        }
    }
}