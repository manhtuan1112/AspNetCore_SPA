using Samples.AspCoreEF.DAL.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.DAL.EF.Models
{
    public class Task : EntityBase
    {
        
        public string Title { set; get; }
        public string Description { set; get; }
     
        public TaskState State { set; get; }
        public virtual Person Person { set; get; }

        public enum TaskState: byte
        {
            Open=0,
            Active=1,
            Completed=2,
            Closed=3
        }

    }
}
