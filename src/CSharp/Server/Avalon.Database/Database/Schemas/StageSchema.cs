namespace Avalon.Database.Schemas
{
    public class StageSchema : FullAbilitySchema
    {
        public string Name { get; set; }
        public byte PlayerCount { get; set; }
        public byte MinionOfMordredCount { get; set; }
        public byte MinionOfMerlinCount { get; set; }
    }
}
