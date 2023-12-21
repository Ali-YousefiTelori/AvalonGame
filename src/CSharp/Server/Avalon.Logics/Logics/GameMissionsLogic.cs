using Avalon.Constants;
using Avalon.Database.Entities;
using Avalon.Database.Entities.Relations;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace Avalon.Logics;
public class GameMissionsLogic
{
    IUnitOfWork _unitOfWork;
    public GameMissionsLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageContract> CreateMissionResult(long gameMissionId, byte failedCount, CancellationToken cancellationToken = default)
    {
        var roleGameLogic = _unitOfWork.GetLongLogic<OfflineGameMissionEntity>();
        var gameMission = await roleGameLogic
            .GetById(gameMissionId, cancellationToken)
            .AsCheckedResult();
        if (gameMission.IsFailed.HasValue)
            return true;

        if (gameMission.DoNeedsTwoOfFails && gameMission.FailCount >= 2)
            gameMission.IsFailed = true;
        else if (!gameMission.DoNeedsTwoOfFails && gameMission.FailCount >= 1)
            gameMission.IsFailed = true;
        else
            gameMission.IsFailed = false;

        gameMission.FailCount = failedCount;

        return await roleGameLogic
             .Update(gameMission, cancellationToken);
    }

    public async Task<MessageContract> SentMissionVote(long gameMissionId, long profileId, bool isFail, CancellationToken cancellationToken = default)
    {
        var gameMissionLogic = _unitOfWork.GetLongLogic<OfflineGameMissionEntity>();
        var gameMissionProfileLogic = _unitOfWork.GetLogic<OfflineGameMissionProfileEntity>();
        var gameMissionProfile = await gameMissionProfileLogic
            .GetBy(x => x.OfflineGameMissionId == gameMissionId && x.ProfileId == profileId,
            q => q.Include(x => x.OfflineGameMission), cancellationToken);
        if (gameMissionProfile)
            return true;

        var mission = await gameMissionLogic
            .GetById(gameMissionId)
            .AsCheckedResult();

        await gameMissionProfileLogic.Add(new OfflineGameMissionProfileEntity()
        {
            ProfileId = profileId,
            OfflineGameMissionId = gameMissionId,
            IsFail = isFail
        }, cancellationToken).AsCheckedResult();
        var gameId = mission.OfflineGameId;
        var votedMission = await gameMissionProfileLogic
            .GetAll(q => q.Where(x => x.OfflineGameMissionId == gameMissionId), cancellationToken)
            .AsCheckedResult();
        if (mission.PlayerCount == votedMission.Count)
            return await CreateMissionResult(gameMissionId, (byte)votedMission.Count(x => x.IsFail.Value), cancellationToken);
        return true;
    }

    public async Task<MessageContract<string>> FinishUp(long gameId, long guessMerlinProfileId, CancellationToken cancellationToken = default)
    {
        var finishUpGameLogic = _unitOfWork.GetLongLogic<FinishUpGameEntity>();
        var gameLogic = _unitOfWork.GetLongLogic<OfflineGameEntity>();

        var game = await gameLogic
            .GetById(gameId, q => q
                .Include(x => x.OfflineGameProfileRoles)
                .ThenInclude(x => x.Profile)
                .Include(x => x.OfflineGameProfileRoles)
                .ThenInclude(x => x.Role)
            , cancellationToken)
            .AsCheckedResult();
        var merlinProfile = game.OfflineGameProfileRoles.FirstOrDefault(x => x.Role.Name == RoleConstants.Merlin);

        var gameMissionProfile = await finishUpGameLogic
            .GetBy(x => x.OfflineGameId == gameId, cancellationToken: cancellationToken);

        if (gameMissionProfile)
            return (FailedReasonType.Duplicate, $"You previously guessed Merlin with {game.OfflineGameProfileRoles.FirstOrDefault(x => x.ProfileId == gameMissionProfile.Result.ProfileId).Profile.Name}’s profile; Merlin’s profile is {merlinProfile.Profile.Name}.");

        await finishUpGameLogic.Add(new FinishUpGameEntity()
        {
            ProfileId = guessMerlinProfileId,
            OfflineGameId = gameId
        }, cancellationToken).AsCheckedResult();

        return merlinProfile.Profile.Name;
    }
}
