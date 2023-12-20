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

        gameMission.FailCount = failedCount;

        return await roleGameLogic.Update(gameMission, cancellationToken);
    }

    public async Task<MessageContract> SentMissionVote(long gameMissionId, long profileId, bool isFail, CancellationToken cancellationToken = default)
    {
        var gameMissionProfileLogic = _unitOfWork.GetLongLogic<OfflineGameMissionProfileEntity>();
        var gameMissionProfile = await gameMissionProfileLogic
            .GetBy(x => x.OfflineGameMissionId == gameMissionId && x.ProfileId == profileId, null, cancellationToken);
        if (gameMissionProfile)
            return true;

        return await gameMissionProfileLogic.Add(new OfflineGameMissionProfileEntity()
        {
            ProfileId = profileId,
            OfflineGameMissionId = gameMissionId,
            IsFail = isFail
        }, cancellationToken);
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
            .GetBy(x => x.OfflineGameId == gameId, q => q.Include(x => x.Profile), cancellationToken);
        if (gameMissionProfile)
            return (FailedReasonType.Duplicate, $"You previously guessed Merlin with {gameMissionProfile.Result.Profile.Name}’s profile; Merlin’s profile is {merlinProfile.Profile.Name}.");

        await finishUpGameLogic.Add(new FinishUpGameEntity()
        {
            GuessMerlinProfileId = guessMerlinProfileId,
            OfflineGameId = gameId
        }, cancellationToken).AsCheckedResult();

        return merlinProfile.Profile.Name;
    }
}
