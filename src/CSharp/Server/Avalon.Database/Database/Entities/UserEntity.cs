﻿using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    public class UserEntity : UserSchema, IIdSchema<long>
    {
        public long Id { get; set; }

        public ICollection<ProfileEntity> Profiles { get; set; }
        public ICollection<OfflineGameEntity> OfflineGames { get; set; }
    }
}
