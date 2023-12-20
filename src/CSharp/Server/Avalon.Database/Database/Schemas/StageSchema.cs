namespace Avalon.Database.Schemas
{
    public class StageSchema : FullAbilitySchema
    {
        public string Name { get; set; }
        public byte PlayerCount { get; set; }
        public byte MinionOfMordredCount { get; set; }
        public byte MinionOfMerlinCount { get; set; }

        public byte Mission1PlayerCount { get; set; }
        public byte Mission2PlayerCount { get; set; }
        public byte Mission3PlayerCount { get; set; }
        public byte Mission4PlayerCount { get; set; }
        public byte Mission5PlayerCount { get; set; }
        public bool DoNeedsTwoOfFailsAtMission4 { get; set; }
    }
}
