using Avalon.Logics;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.Cores.Interfaces;
using System.Security.Claims;

namespace Avalon.Models
{
    public class AppUnitOfWork : UnitOfWork
    {
        public AppUnitOfWork(IServiceProvider service) : base(service)
        {
        }

        public GameCreatorLogic GetGameCreatorLogic()
        {
            return ServiceProvider.GetService<GameCreatorLogic>();
        }

        public CurrentUser GetCurrentUser()
        {
            var context = ServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext;
            return new CurrentUser()
            {
                 UniqueIdentity = context.User.FindFirstValue(nameof(IUniqueIdentitySchema.UniqueIdentity)),
            };
        }

        public GameMissionsLogic GetGameMissionsLogic()
        {
            return ServiceProvider.GetService<GameMissionsLogic>();
        }
    }
}
