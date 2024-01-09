namespace Avalon.Database.Entities.Relations
{
    public class OfflineGameMissionProfileEntity : FullAbilitySchema
    {
        public long OfflineGameMissionId { get; set; }
        public long AvalonProfileId { get; set; }
        /// <summary>
        /// vote of profile in mission
        /// </summary>
        public bool? IsFail { get; set; }
        public OfflineGameMissionEntity OfflineGameMission { get; set; }
        public AvalonProfileEntity AvalonProfile { get; set; }
    }
}
