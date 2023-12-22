using EasyMicroservices.Cores.Database.Schemas;

namespace Avalon.Contracts.Common;

public class ProfileBaseContract : FullAbilitySchema
{
    public string Name { get; set; }
}
