using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    public class StageEntity : StageSchema
    {
        public byte PlayerCount { get; set; }
        public ICollection<OfflineGameEntity> OfflineGames { get; set; }
    }
}
