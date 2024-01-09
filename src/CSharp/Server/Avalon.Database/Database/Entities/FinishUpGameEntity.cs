namespace Avalon.Database.Entities;
public class FinishUpGameEntity : FullAbilityIdSchema<long>
{
    /// <summary>
    /// Guess Merlin ProfileId
    /// </summary>
    public long AvalonProfileId { get; set; }
    public AvalonProfileEntity AvalonProfile { get; set; }

    public long OfflineGameId { get; set; }
    public OfflineGameEntity OfflineGame { get; set; }
}
