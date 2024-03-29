﻿using Avalon.Constants;
using Avalon.Database.Entities;
using Avalon.Database.Entities.Relations;
using Avalon.Logics;
using Avalon.Tests.Fixtures;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Contracts.Requests;
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
    [InlineData(11, 6, 5)]
    [InlineData(12, 7, 5)]
    [InlineData(13, 7, 6)]
    [InlineData(14, 8, 6)]
    [InlineData(15, 8, 7)]
    public async Task DoGameAsync(byte playerCount, byte peopleCount, byte minionOfMordredCount)
    {
        #region create game
        var myProfiles = await _unitOfWork.GetLongLogic<AvalonProfileEntity>()
        .GetAllByUniqueIdentity(new GetByUniqueIdentityRequestContract()
        {
            UniqueIdentity = "1-2"
        }, EasyMicroservices.Cores.DataTypes.GetUniqueIdentityType.All)
            .AsCheckedResult();
        var gameId = await _gameCreatorLogic.CreateNew("1-2", myProfiles.Take(playerCount).ToList());
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
                .Include(x => x.AvalonRole)
                .Include(x => x.AvalonProfile))
            .AsCheckedResult();
        Assert.True(profiles.Count == stage.PlayerCount);
        Assert.True(profiles.Count(x => x.AvalonRole.IsMinionOfMordred) == stage.MinionOfMordredCount);
        Assert.True(profiles.Count(x => !x.AvalonRole.IsMinionOfMordred) == stage.MinionOfMerlinCount);
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
                     .SentMissionVote(gameMission.Id, missionProfile.Id, missionProfile.AvalonRole.IsMinionOfMordred)
                     .AsCheckedResult();
            }
            var notCachedMissionLogic = _unitOfWork.GetLongLogic<OfflineGameMissionEntity>();
            var mission = await notCachedMissionLogic
                .GetById(gameMission.Id)
                .AsCheckedResult();
            Assert.True(mission.IsFailed.HasValue);
            Assert.Equal(mission.FailCount, missionProfiles.Count(x => x.AvalonRole.IsMinionOfMordred));
        }
        #endregion

        #region finishup game
        var merlin = profiles.FirstOrDefault(x => x.AvalonRole.Name == RoleConstants.Merlin);
        var gameResult = await _gameMissionsLogic
            .FinishUp(gameId, merlin.AvalonProfileId)
            .AsCheckedResult();
        Assert.Equal(merlin.AvalonProfile.Name, gameResult);
        var gameResultAgain = await _gameMissionsLogic
            .FinishUp(gameId, merlin.AvalonProfileId);
        var all = await _unitOfWork.GetLogic<FinishUpGameEntity>().GetAll();
        Assert.True(gameResultAgain.Error.FailedReasonType == FailedReasonType.Duplicate);
        #endregion
    }
}
