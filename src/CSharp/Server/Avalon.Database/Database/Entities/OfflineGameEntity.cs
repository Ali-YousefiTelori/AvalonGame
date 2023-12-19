using Avalon.Database.Entities.Relations;
using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    public class OfflineGameEntity : OfflineGameSchema, IIdSchema<long>
    {
        public long Id { get; set; }
        public long CreatorUserId { get; set; }
        public long StageId { get; set; }
        public UserEntity CreatorUser { get; set; }
        public StageEntity Stage { get; set; }

        public ICollection<OfflineGameProfileRoleEntity> OfflineGameProfileRoles { get; set; }
        public ICollection<OfflineGameMissionEntity> OfflineGameMissions { get; set; }
    }
    
}
