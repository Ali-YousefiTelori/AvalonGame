using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalon.Database.Schemas
{
    public class OfflineGameSchema : IUniqueIdentitySchema, IDateTimeSchema, ISoftDeleteSchema
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public string UniqueIdentity { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
    }
}