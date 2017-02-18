using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;

namespace Samples.AspCoreEF.DAL.EF.Repositories
{
    public interface IApplicationUserGroupRepository : IRepositoryWithoutEntityBase<ApplicationUserGroup>
    {

    }
    public class ApplicationUserGroupRepository : RepositoryBaseWithoutEntityBase<ApplicationUserGroup>, IApplicationUserGroupRepository
    {
        public ApplicationUserGroupRepository(TaskSystemDbContext context) : base(context)
        {
        }
    }
}
