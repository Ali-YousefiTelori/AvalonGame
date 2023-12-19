using Avalon.Database.Entities;
using Avalon.Database.Entities.Relations;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.ServiceContracts;

namespace Avalon.Logics;
public class GameCreatorLogic
{
    IUnitOfWork _unitOfWork;
    public GameCreatorLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<long> CreateNew(long userId, ICollection<ProfileEntity> profiles, CancellationToken cancellationToken = default)
    {
        var stageGameLogic = _unitOfWork.GetLongLogic<StageEntity>();

        var finsStage = await stageGameLogic
            .GetBy(x => x.PlayerCount == profiles.Count, cancellationToken: cancellationToken)
            .AsCheckedResult();

        var offlineGameLogic = _unitOfWork.GetLongLogic<OfflineGameEntity>();
        return await offlineGameLogic.Add(new OfflineGameEntity()
        {
            CreatorUserId = userId,
            StageId = finsStage.Id,
            OfflineGameProfileRoles = await AsignRolesToProfiles(profiles),
        }, cancellationToken);
    }

    public async Task<List<OfflineGameProfileRoleEntity>> AsignRolesToProfiles(ICollection<ProfileEntity> profiles, CancellationToken cancellationToken = default)
    {
        var roleGameLogic = _unitOfWork.GetLongLogic<RoleEntity>();
        var roles = await roleGameLogic
            .GetAll(cancellationToken)
            .AsCheckedResult();
        return profiles.Select(x => new OfflineGameProfileRoleEntity()
        {
            Roled = roles.FirstOrDefault().Id,
            ProfileId = x.Id
        }).ToList();
    }
}