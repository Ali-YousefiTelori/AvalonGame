using Avalon.Database.Entities.Relations;
using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    /// <summary>
    /// profiles are for offline games
    /// </summary>
    public class ProfileEntity : ProfileSchema, IIdSchema<long>
    {
        public long Id { get; set; }

        public ICollection<OfflineGameProfileRoleEntity> OfflineGameProfileRoles { get; set; }
        public ICollection<OfflineGameMissionProfileEntity> OfflineGameMissionProfiles { get; set; }
        public ICollection<FinishUpGameEntity> FinishUpGames { get; set; }
    }
}
