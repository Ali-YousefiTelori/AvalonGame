namespace Avalon.Database.Schemas
{
    /// <summary>
    /// every mission selection has 5 phases
    /// when phases finished, mordred wins
    /// </summary>
    public class PhaseSchema : IUniqueIdentitySchema, IDateTimeSchema, ISoftDeleteSchema
    {
        /// <summary>
        /// level and index of phase
        /// </summary>
        public byte Index { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public string UniqueIdentity { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
    }
}
