namespace Avalon.Database.Entities.Relations
{
    public class OfflineGameProfileRoleEntity
    {
        public long Id { get; set; }
        public long OfflineGameId { get; set; }
        public long ProfileId { get; set; }
        /// <summary>
        /// it will random generated after game start
        /// </summary>
        public long? Roled { get; set; }

        public OfflineGameEntity OfflineGame { get; set; }
        public ProfileEntity Profile { get; set; }
        public RoleEntity Role { get; set; }
    }
}
