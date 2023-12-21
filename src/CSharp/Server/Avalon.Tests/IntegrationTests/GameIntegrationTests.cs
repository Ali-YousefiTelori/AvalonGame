using Avalon.Constants;
using Avalon.Database.Entities;
using Avalon.Database.Entities.Relations;
using Avalon.Logics;
using Avalon.Tests.Fixtures;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Avalon.Tests.IntegrationTests;
public class GameIntegrationTests : IClassFixture<UnitTestsFixture>, IClassFixture<WhiteLabelLaboratoryFixture>
{
    IUnitOfWork _unitOfWork;
    GameCreatorLogic _gameCreatorLogic;
    GameMissionsLogic _gameMissionsLogic;
    public GameIntegrationTests(UnitTestsFixture unitTestsFixture)
    {
        _unitOfWork = unitTestsFixture.ServiceProvider.GetService<IUnitOfWork>();
        _gameCreatorLogic = unitTestsFixture.ServiceProvider.GetService<GameCreatorLogic>();
        _gameMissionsLogic = unitTestsFixture.ServiceProvider.GetService<GameMissionsLogic>();
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(6, 4, 2)]
    [InlineData(7, 4, 3)]
    [InlineData(8, 5, 3)]
    [InlineData(9, 6, 3)]
    [InlineData(10, 6, 4)]
    public async Task DoGameAsync(byte playerCount, byte peopleCount, byte minionOfMordredCount)
    {
        #region create game
        var user = await _unitOfWork.GetLongLogic<UserEntity>()
        .GetById(1, q => q.Include(x => x.Profiles))
            .AsCheckedResult();
        var gameId = await _gameCreatorLogic.CreateNew(user.Id, user.Profiles.Take(playerCount).ToList());
        Assert.True(gameId > 0);

        var game = await _unitOfWork.GetLongLogic<OfflineGameEntity>()
            .GetById(gameId)
            .AsCheckedResult();

        var stage = await _unitOfWork.GetLongLogic<StageEntity>()
            .GetById(game.StageId)
            .AsCheckedResult();

        Assert.True(stage.MinionOfMordredCount == minionOfMordredCount);
        Assert.True(stage.MinionOfMerlinCount == peopleCount);

        var profiles = await _unitOfWork.GetLongLogic<OfflineGameProfileRoleEntity>()
            .GetAll(q => q.Where(x => x.OfflineGameId == gameId)
                .Include(x => x.Role)
                .Include(x => x.Profile))
            .AsCheckedResult();
        Assert.True(profiles.Count == stage.PlayerCount);
        Assert.True(profiles.Count(x => x.Role.IsMinionOfMordred) == stage.MinionOfMordredCount);
        Assert.True(profiles.Count(x => !x.Role.IsMinionOfMordred) == stage.MinionOfMerlinCount);
        #endregion

        #region do missions
        var missionLogic = _unitOfWork.GetLongLogic<OfflineGameMissionEntity>();
        var gameMissions = await missionLogic
            .GetAll(q => q.Where(x => x.OfflineGameId == gameId))
            .AsCheckedResult();
        Assert.Equal(5, gameMissions.Count);
        foreach (var gameMission in gameMissions)
        {
            var missionProfiles = profiles.OrderBy(x => Guid.NewGuid()).Take(gameMission.PlayerCount).ToList();
            foreach (var missionProfile in missionProfiles)
            {
                await _gameMissionsLogic
                     .SentMissionVote(gameMission.Id, missionProfile.Id, missionProfile.Role.IsMinionOfMordred)
                     .AsCheckedResult();
            }
            var notCachedMissionLogic = _unitOfWork.GetLongLogic<OfflineGameMissionEntity>();
            var mission = await notCachedMissionLogic
                .GetById(gameMission.Id)
                .AsCheckedResult();
            Assert.True(mission.IsFailed.HasValue);
            Assert.Equal(mission.FailCount, missionProfiles.Count(x => x.Role.IsMinionOfMordred));
        }
        #endregion

        #region finishup game
        var merlin = profiles.FirstOrDefault(x => x.Role.Name == RoleConstants.Merlin);
        var gameResult = await _gameMissionsLogic
            .FinishUp(gameId, merlin.ProfileId)
            .AsCheckedResult();
        Assert.Equal(merlin.Profile.Name, gameResult);
        var gameResultAgain = await _gameMissionsLogic
            .FinishUp(gameId, merlin.ProfileId);
        var all = await _unitOfWork.GetLogic<FinishUpGameEntity>().GetAll();
        Assert.True(gameResultAgain.Error.FailedReasonType == FailedReasonType.Duplicate);
        #endregion
    }
}
