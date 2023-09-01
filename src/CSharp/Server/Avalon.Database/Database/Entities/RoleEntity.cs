using Avalon.Database.Entities.Relations;
using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    public class RoleEntity : RoleSchema
    {
        public long Id { get; set; }
        public ICollection<OfflineGameProfileRoleEntity> OfflineGameProfileRoles { get; set; }
    }
}
