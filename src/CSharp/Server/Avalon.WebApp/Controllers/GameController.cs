using Avalon.Contracts.Common;
using Avalon.Contracts.Requests;
using Avalon.Contracts.Responses;
using Avalon.Database.Entities;
using Avalon.Database.Entities.Relations;
using Avalon.Models;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Avalon.WebApp.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class GameController : ControllerBase
{
    readonly AppUnitOfWork _appUnitOfWork;
    public GameController(AppUnitOfWork appUnitOfWork)
    {
        _appUnitOfWork = appUnitOfWork;
    }

    [HttpPost]
    public async Task<MessageContract<CreateGameResponseContract>> CreateGame(CreateGameRequestContract createGameRequest, CancellationToken cancellationToken)
    {
        var logic = _appUnitOfWork.GetGameCreatorLogic();
        var profileLogic = _appUnitOfWork.GetLongLogic<ProfileEntity>();
        var gameProfileRoleLogic = _appUnitOfWork.GetLongLogic<OfflineGameProfileRoleEntity>();

        var profiles = await profileLogic
            .GetAll(q => q.Where(x => createGameRequest.Profiles.Contains(x.Id)), cancellationToken)
            .AsCheckedResult();

        if (profiles.Count < 5)
            return (FailedReasonType.ValidationsError, "Maximum profile count: 5");

        var gameId = await logic.CreateNew(1, profiles, cancellationToken);

        List<OfflineGameProfileRoleEntity> result = new List<OfflineGameProfileRoleEntity>();
        var gameProfiles = await gameProfileRoleLogic.GetAll(q => q.Where(x => x.OfflineGameId == gameId)
                                                .Include(x => x.Role)
                                                .Include(x => x.Profile), cancellationToken)
                                                .AsCheckedResult();

        return new CreateGameResponseContract()
        {
            GameId = gameId,
            GameProfiles = gameProfiles.Select(x => new GameProfileContract()
            {
                ProfileId = x.ProfileId,
                RoleName = x.Role.Name,
                IsMinionOfMordred = x.Role.IsMinionOfMordred
            }).ToList()
        };
    }

    [HttpPost]
    public async Task<ListMessageContract<OfflineGameMissionContract>> GetGameMissions(GetByIdRequestContract<long> getByIdRequest, CancellationToken cancellationToken)
    {
        var gameLogic = _appUnitOfWork.GetLongContractLogic<OfflineGameMissionEntity, OfflineGameMissionContract>();
        return await gameLogic
            .GetAll(q => q.Where(x => x.OfflineGameId == getByIdRequest.Id), cancellationToken: cancellationToken);
    }

    [HttpPost]
    public Task<MessageContract> CreateMissionResult(CreateGaneMissionRequestContract createGaneMissionRequest, CancellationToken cancellationToken)
    {
        var missionLogic = _appUnitOfWork.GetGameMissionsLogic();
        return missionLogic.CreateMissionResult(createGaneMissionRequest.GameMissionId, createGaneMissionRequest.FailCount, cancellationToken);
    }
}
