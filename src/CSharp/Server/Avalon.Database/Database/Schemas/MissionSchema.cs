namespace Avalon.Database.Schemas
{
    public class MissionSchema : DateTimeSchema
    {
        public byte Index { get; set; }
        /// <summary>
        /// null = is not started
        /// </summary>
        public bool? IsFailed { get; set; }
    }
}