using Avalon.Database.Schemas;

namespace Avalon.Database.Entities;
public class UserFeedbackEntity : UserFeedbackSchema, IIdSchema<long>
{
    public long Id { get; set; }
}
