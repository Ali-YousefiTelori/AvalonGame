namespace Avalon.Database.Schemas
{
    /// <summary>
    /// every mission selection has 5 phases
    /// when phases finished, mordred wins
    /// </summary>
    public class PhaseSchema : FullAbilitySchema
    {
        /// <summary>
        /// level and index of phase
        /// </summary>
        public byte Index { get; set; }
    }
}
