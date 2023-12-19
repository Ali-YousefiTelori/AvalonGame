using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    /// <summary>
    /// Stages are game boards, each game board has 5 mission
    /// </summary>
    public class StageEntity : StageSchema, IIdSchema<long>
    {
        public long Id { get; set; }
        public ICollection<OfflineGameEntity> OfflineGames { get; set; }
    }
}
