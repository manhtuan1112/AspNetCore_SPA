using Samples.AspCoreEF.DAL.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.DAL.EF.Models
{
    public class Person : EntityBase
    {
        public string Name { set; get; }
    }
}
