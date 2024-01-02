using Avalon.Logics;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;

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
            //context.User.Claims
            return null;
        }

        public GameMissionsLogic GetGameMissionsLogic()
        {
            return ServiceProvider.GetService<GameMissionsLogic>();
        }
    }
}
