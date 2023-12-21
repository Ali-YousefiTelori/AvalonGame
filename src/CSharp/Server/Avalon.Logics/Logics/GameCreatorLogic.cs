using Avalon.Constants;
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

        var findStage = await stageGameLogic
            .GetBy(x => x.PlayerCount == profiles.Count, cancellationToken: cancellationToken)
            .AsCheckedResult();

        var offlineGameLogic = _unitOfWork.GetLongLogic<OfflineGameEntity>();
        return await offlineGameLogic.Add(new OfflineGameEntity()
        {
            CreatorUserId = userId,
            StageId = findStage.Id,
            OfflineGameProfileRoles = await AsignRolesToProfiles(profiles, findStage.MinionOfMerlinCount, findStage.MinionOfMordredCount),
            OfflineGameMissions = new List<OfflineGameMissionEntity>()
            {
                new OfflineGameMissionEntity()
                {
                     Index = 1,
                     PlayerCount = findStage.Mission1PlayerCount
                },
                new OfflineGameMissionEntity()
                {
                     Index = 2,
                     PlayerCount = findStage.Mission2PlayerCount
                },
                new OfflineGameMissionEntity()
                {
                     Index = 3,
                     PlayerCount = findStage.Mission3PlayerCount
                },
                new OfflineGameMissionEntity()
                {
                     Index = 4,
                     PlayerCount = findStage.Mission4PlayerCount,
                     DoNeedsTwoOfFails = findStage.DoNeedsTwoOfFailsAtMission4
                },
                new OfflineGameMissionEntity()
                {
                     Index = 5,
                     PlayerCount = findStage.Mission5PlayerCount
                }
            }
        }, cancellationToken);
    }

    public async Task<List<OfflineGameProfileRoleEntity>> AsignRolesToProfiles(ICollection<ProfileEntity> profiles, byte minionOfMerlinCount, byte minionOfMordredCount, CancellationToken cancellationToken = default)
    {
        var roleGameLogic = _unitOfWork.GetLongLogic<RoleEntity>();
        var roles = await roleGameLogic
            .GetAll(cancellationToken)
            .AsCheckedResult();
        var minionOfMerlin = roles.Where(x => !x.IsMinionOfMordred)
            .OrderByDescending(x => x.Name == RoleConstants.Merlin || x.Name == RoleConstants.Percival)
            .Take(minionOfMerlinCount);

        var minionOfMordred = roles.Where(x => x.IsMinionOfMordred)
            .OrderBy(x => x.Name == RoleConstants.Oberon)
            .OrderByDescending(x => x.Name == RoleConstants.Mordred ? 3 : x.Name == RoleConstants.Morgana ? 2 : x.Name == RoleConstants.Assassin ? 1 : 0)
            .Take(minionOfMordredCount);
        //merge and randomize the players
        var merge = minionOfMerlin.Concat(minionOfMordred).OrderBy(x => Guid.NewGuid()).ToList();
        List<OfflineGameProfileRoleEntity> result = new List<OfflineGameProfileRoleEntity>();

        foreach (var item in profiles)
        {
            var role = merge.FirstOrDefault();
            merge.Remove(role);
            result.Add(new OfflineGameProfileRoleEntity()
            {
                Roled = role.Id,
                ProfileId = item.Id
            });
        }
        return result;
    }
}