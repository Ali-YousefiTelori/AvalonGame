using Avalon.Database.Schemas;

namespace Avalon.Database.Entities;
public class AvalonUserFeedbackEntity : UserFeedbackSchema, IIdSchema<long>
{
    public long Id { get; set; }
}
