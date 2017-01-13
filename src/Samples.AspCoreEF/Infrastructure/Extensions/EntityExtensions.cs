using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.Id = product.Id;
            product.Name = productVm.Name;
            product.Alias = productVm.Alias;
            product.CategoryID = productVm.CategoryID;
            product.Description = productVm.Description;
            product.Image = productVm.Image;
            product.MoreImages = productVm.MoreImages;
            product.Price = productVm.Price;
            product.PromotionPrice = productVm.PromotionPrice;
            product.Warranty = productVm.Warranty;
            product.Content = productVm.Content;
            product.HomeFlag = productVm.HomeFlag;
            product.HotFlag = productVm.HotFlag;
            product.ViewCount = productVm.ViewCount;
            product.OriginalPrice = productVm.OriginalPrice;

            product.AddedDate = productVm.AddedDate;
            product.ModifiedDate = productVm.UpdatedDate;
            
            product.Status = productVm.Status;
            product.Tags = productVm.Tags;
            product.Quantity = productVm.Quantity;
            
        }
    }
}
