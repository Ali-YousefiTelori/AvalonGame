using Avalon.Database.Entities;
using Avalon.Database.Entities.Relations;
using Avalon.Logics;
using Avalon.Tests.Fixtures;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Avalon.Tests.UnitTests;
public class GameCreatorLogicUnitTests : IClassFixture<UnitTestsFixture>, IClassFixture<WhiteLabelLaboratoryFixture>
{
    GameCreatorLogic _gameCreatorLogic;
    IUnitOfWork _unitOfWork;
    public GameCreatorLogicUnitTests(UnitTestsFixture unitTestsFixture)
    {
        _unitOfWork = unitTestsFixture.ServiceProvider.GetService<IUnitOfWork>();
        _gameCreatorLogic = unitTestsFixture.ServiceProvider.GetService<GameCreatorLogic>();
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(6, 4, 2)]
    [InlineData(7, 4, 3)]
    [InlineData(8, 5, 3)]
    [InlineData(9, 6, 3)]
    [InlineData(10, 6, 4)]
    public async Task CreateGameAsync(byte playerCount, byte peopleCount, byte minionOfMordredCount)
    {
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
            .GetAll(q => q.Where(x => x.OfflineGameId == gameId).Include(x => x.Role))
            .AsCheckedResult();
        Assert.True(profiles.Count == stage.PlayerCount);
        Assert.True(profiles.Count(x => x.Role.IsMinionOfMordred) == stage.MinionOfMordredCount);
        Assert.True(profiles.Count(x => !x.Role.IsMinionOfMordred) == stage.MinionOfMerlinCount);
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(6, 4, 2)]
    [InlineData(7, 4, 3)]
    [InlineData(8, 5, 3)]
    [InlineData(9, 6, 3)]
    [InlineData(10, 6, 4)]
    public async Task AsignRolesToProfilesAsync(byte playerCount, byte peopleCount, byte minionOfMordredCount)
    {
        var user = await _unitOfWork.GetLongLogic<UserEntity>()
            .GetById(1, q => q.Include(x => x.Profiles))
            .AsCheckedResult();
        var profileRoles = await _gameCreatorLogic.AsignRolesToProfiles(user.Profiles.Take(playerCount).ToList(), peopleCount, minionOfMordredCount);
        Assert.True(profileRoles.Count == playerCount);
        foreach (var item in profileRoles.GroupBy(x => x.Roled))
        {
            Assert.True(item.Count() == 1);
        }
        foreach (var item in profileRoles.GroupBy(x => x.ProfileId))
        {
            Assert.True(item.Count() == 1);
        }
    }
}
