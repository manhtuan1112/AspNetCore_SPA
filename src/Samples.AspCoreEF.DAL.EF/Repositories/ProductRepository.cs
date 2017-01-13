using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;

namespace Samples.AspCoreEF.DAL.EF.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void SaveChange();
    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private TaskSystemDbContext _context;
        public ProductRepository(TaskSystemDbContext context) : base(context)
        {
            this._context = context;
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
}
