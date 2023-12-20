namespace Avalon.Database.Entities;
public class FinishUpGameEntity : FullAbilityIdSchema<long>
{
    public long GuessMerlinProfileId { get; set; }
    public ProfileEntity Profile { get; set; }

    public long OfflineGameId { get; set; }
    public OfflineGameEntity OfflineGame { get; set; }
}
