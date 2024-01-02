using Avalon.Contracts.Common;
using Avalon.Database.Entities;
using Avalon.WebApp.Attributes;
using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
namespace Avalon.WebApp.Controllers;

[AvalonSecurity]
public class ProfileController : SimpleQueryServiceController<ProfileEntity, ProfileBaseContract, ProfileContract, ProfileContract, long>
{
    public ProfileController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
