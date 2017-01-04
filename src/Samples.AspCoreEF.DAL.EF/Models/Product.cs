using Samples.AspCoreEF.DAL.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.DAL.EF.Models
{
    [Table("Products")]
    public class Product : EntityBase
    {
     
        [Required]
        public string Name { set; get; }

        [Required]
        public string Alias { set; get; }

        public long CategoryID { set; get; }
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }
        public decimal OriginalPrice { set; get; }
        public decimal Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public int? Warranty { set; get; }
        public string Description { set; get; }
        public string Content { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        public string Tags { set; get; }
        public int Quantity { set; get; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { set; get; }

        
    }
}
