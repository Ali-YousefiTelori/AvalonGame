namespace Avalon.Database.Schemas
{
    public class RoleSchema : IUniqueIdentitySchema, IDateTimeSchema, ISoftDeleteSchema
    {
        public string Name { get; set; }
        public bool IsMinionOfMordred { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public string UniqueIdentity { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
    }
}

