using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;

namespace Samples.AspCoreEF.DAL.EF.Repositories
{
    public interface IProductTagRepository : IRepositoryWithoutEntityBase<ProductTag>
    {

    }
    public class ProductTagRepository : RepositoryBaseWithoutEntityBase<ProductTag>, IProductTagRepository
    {
        public ProductTagRepository(TaskSystemDbContext context) : base(context)
        {
        }
    }
}
