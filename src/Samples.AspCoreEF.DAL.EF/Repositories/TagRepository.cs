using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;

namespace Samples.AspCoreEF.DAL.EF.Repositories
{
    public interface ITagRepository : IRepositoryWithoutEntityBase<Tag> {

    }
    public class TagRepository : RepositoryBaseWithoutEntityBase<Tag>, ITagRepository
    {
        public TagRepository(TaskSystemDbContext context) : base(context)
        {
        }
    }
}
