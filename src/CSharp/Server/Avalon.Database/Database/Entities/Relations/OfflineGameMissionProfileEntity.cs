namespace Avalon.Database.Entities.Relations
{
    public class OfflineGameMissionProfileEntity
    {
        public long OfflineGameMissionId { get; set; }
        public long ProfileId { get; set; }
        public bool? IsFail { get; set; }
        public OfflineGameMissionEntity OfflineGameMission { get; set; }
        public ProfileEntity Profile { get; set; }
    }
}
