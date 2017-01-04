using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.DAL.EF.Infrastructure
{
    public class EntityBase
    {
        public Int64 Id { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string IPAddress { get; set; }
    }
}
