using Avalon.Contracts.Common;
using Avalon.Database.Entities;
using Avalon.WebApp.Attributes;
using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Avalon.WebApp.Controllers;

[AvalonSecurity]
public class UserFeedbackController : SimpleQueryServiceController<AvalonUserFeedbackEntity, UserFeedbackContract, UserFeedbackContract, UserFeedbackContract, long>
{
    public UserFeedbackController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    [HttpGet]
    public MessageContract<string> GetGlobalMessage()
    {
        return "به بازی خوش آمدید.";
    }
}
