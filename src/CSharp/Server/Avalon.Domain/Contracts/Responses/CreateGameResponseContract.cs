using Avalon.Contracts.Common;

namespace Avalon.Contracts.Responses;
public class CreateGameResponseContract
{
    public long GameId { get; set; }
    public List<GameProfileContract> GameProfiles { get; set; }
}
