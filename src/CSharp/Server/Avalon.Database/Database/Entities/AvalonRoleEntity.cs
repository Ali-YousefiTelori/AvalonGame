﻿using Avalon.Database.Entities.Relations;
using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    public class AvalonRoleEntity : RoleSchema, IIdSchema<long>
    {
        public long Id { get; set; }
        public ICollection<OfflineGameProfileRoleEntity> OfflineGameProfileRoles { get; set; }
    }
}
