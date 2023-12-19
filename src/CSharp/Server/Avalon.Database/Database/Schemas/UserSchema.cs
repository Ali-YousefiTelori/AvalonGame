namespace Avalon.Database.Schemas
{
    public class UserSchema : FullAbilitySchema
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public Guid Key { get; set; } = Guid.NewGuid();
    }
}
