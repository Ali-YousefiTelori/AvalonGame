using Avalon.Database.Entities;
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

    [Fact]
    public async Task CreateGameAsync()
    {
        var user = await _unitOfWork.GetLongLogic<UserEntity>()
            .GetById(1, q => q.Include(x => x.Profiles))
            .AsCheckedResult();
        var gameId = await _gameCreatorLogic.CreateNew(user.Id, user.Profiles);
        Assert.True(gameId > 0);
    }
}
