namespace Avalon.Database.Entities;
public class FinishUpGameEntity : FullAbilityIdSchema<long>
{
    /// <summary>
    /// Guess Merlin ProfileId
    /// </summary>
    public long ProfileId { get; set; }
    public ProfileEntity Profile { get; set; }

    public long OfflineGameId { get; set; }
    public OfflineGameEntity OfflineGame { get; set; }
}
