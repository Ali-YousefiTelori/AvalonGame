namespace Avalon.Database.Schemas
{
    public class MissionSchema :  IDateTimeSchema
    {
        public bool? IsFailed { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
    }
}