using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    public class UserEntity : UserSchema
    {
        public long Id { get; set; }

        public ICollection<ProfileEntity> Profiles { get; set; }
    }
}
