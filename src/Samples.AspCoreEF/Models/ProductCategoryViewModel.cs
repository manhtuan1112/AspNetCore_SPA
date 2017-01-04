using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Models
{
    public class ProductCategoryViewModel
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }

        public string Description { set; get; }
        public long? ParentID { set; get; }
        public int? DisplayOrder { set; get; }
        public string Image { set; get; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? Status { set; get; }
        public virtual IEnumerable<ProductViewModel> Products { set; get; }

    }
}
