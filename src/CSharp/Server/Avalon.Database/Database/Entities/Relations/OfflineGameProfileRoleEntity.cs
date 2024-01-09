namespace Avalon.Database.Entities.Relations
{
    public class OfflineGameProfileRoleEntity : FullAbilityIdSchema<long>
    {
        public long OfflineGameId { get; set; }
        public long AvalonProfileId { get; set; }
        /// <summary>
        /// it will random generated after game start
        /// </summary>
        public long? AvalonRoleId { get; set; }

        public OfflineGameEntity OfflineGame { get; set; }
        public AvalonProfileEntity AvalonProfile { get; set; }
        public AvalonRoleEntity AvalonRole { get; set; }
    }
}
