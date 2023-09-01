namespace Avalon.Database.Schemas
{
    public class VoteSchema : IUniqueIdentitySchema, IDateTimeSchema, ISoftDeleteSchema
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public string UniqueIdentity { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }

        public bool IsAccepted { get; set; }
        /// <summary>
        ///user who voted
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// phase that has this vote
        /// </summary>
        public long PhaseId { get; set; }
    }
}
