using Avalon.Contracts.Requests;
using Avalon.Database.Entities;
using Avalon.Models;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Avalon.WebApp.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly AppUnitOfWork _unitOfWork;
        public UserController(AppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<MessageContract> Register(RegisterUserRequestContract registerUserRequest)
        {
            await using (_unitOfWork)
            {
                return await _unitOfWork.GetLongContractLogic<UserEntity, RegisterUserRequestContract>().Add(registerUserRequest);
            }
        }
    }
}
