using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    public class StageEntity : StageSchema, IIdSchema<long>
    {
        public long Id { get; set; }
        public byte PlayerCount { get; set; }
        public ICollection<OfflineGameEntity> OfflineGames { get; set; }
    }
}
