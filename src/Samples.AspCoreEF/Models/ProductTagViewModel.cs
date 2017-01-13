using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Models
{
    public class ProductTagViewModel
    {
        public long ProductID { set; get; }

        public string TagID { set; get; }

        public virtual ProductTagViewModel Product { set; get; }

        public virtual TagViewModel Tag { set; get; }
    }
}
