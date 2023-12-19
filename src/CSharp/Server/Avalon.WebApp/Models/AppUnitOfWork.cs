using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;

namespace Avalon.Models
{
    public class AppUnitOfWork : UnitOfWork
    {
        public AppUnitOfWork(IServiceProvider service) : base(service)
        {
        }
    }
}
