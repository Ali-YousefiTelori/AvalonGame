using Avalon.Constants;
using Avalon.Database.Entities;
using Avalon.Database.Entities.Relations;
using Avalon.Logics;
using Avalon.Tests.Fixtures;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EasyMicroservices.Cores.Contracts.Requests;

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
    [InlineData(11, 6, 5)]
    [InlineData(12, 7, 5)]
    [InlineData(13, 7, 6)]
    [InlineData(14, 8, 6)]
    [InlineData(15, 8, 7)]
    public async Task CreateGameAsync(byte playerCount, byte peopleCount, byte minionOfMordredCount)
    {
        var myProfiles = await _unitOfWork.GetLongLogic<ProfileEntity>()
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
    [InlineData(11, 6, 5)]
    [InlineData(12, 7, 5)]
    [InlineData(13, 7, 6)]
    [InlineData(14, 8, 6)]
    [InlineData(15, 8, 7)]
    public async Task AssignRolesToProfilesAsync(byte playerCount, byte peopleCount, byte minionOfMordredCount)
    {
        var myProfiles = await _unitOfWork.GetLongLogic<ProfileEntity>()
        .GetAllByUniqueIdentity(new GetByUniqueIdentityRequestContract()
        {
            UniqueIdentity = "1-2"
        }, EasyMicroservices.Cores.DataTypes.GetUniqueIdentityType.All)
            .AsCheckedResult();
        var roles = await _unitOfWork.GetLongLogic<RoleEntity>()
            .GetAll()
            .AsCheckedResult();
        var profileRoles = await _gameCreatorLogic.AsignRolesToProfiles(myProfiles.Take(playerCount).ToList(), peopleCount, minionOfMordredCount);
        foreach (var item in profileRoles)
        {
            item.Role = roles.FirstOrDefault(x=>x.Id == item.Roled);
        }
        Assert.True(profileRoles.Count == playerCount);
        Assert.Contains(profileRoles, x => x.Role.Name == RoleConstants.Merlin);
        Assert.Contains(profileRoles, x => x.Role.Name == RoleConstants.Percival);
        Assert.Contains(profileRoles, x => x.Role.Name == RoleConstants.Mordred);
        if (playerCount == 5 || playerCount == 6)
        {
            Assert.Contains(profileRoles, x => x.Role.Name == RoleConstants.Morgana || x.Role.Name == RoleConstants.Assassin);
        }
        else
        {
            Assert.Contains(profileRoles, x => x.Role.Name == RoleConstants.Morgana);
            Assert.Contains(profileRoles, x => x.Role.Name == RoleConstants.Assassin);
        }
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
