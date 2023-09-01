using Avalon.Database.Schemas;

namespace Avalon.Database.Entities
{
    public class StageEntity : StageSchema
    {
        public long Id { get; set; }
        public byte PlayerCount { get; set; }
    }
}
