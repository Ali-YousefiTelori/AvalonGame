using Avalon.Database.Schemas;

namespace Avalon.Database.Entities.Relations
{
    public class OfflineGameMissionEntity : MissionSchema
    {
        public long Id { get; set; }
        public long OfflineGameId { get; set; }

        public OfflineGameEntity OfflineGame { get; set; }
        public ICollection<OfflineGameMissionProfileEntity> OfflineGameMissionProfiles { get; set; }
    }
}
