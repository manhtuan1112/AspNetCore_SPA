using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;
using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.DAL.EF.Repositories
{
    public interface IApplicationRoleGroupRepository : IRepositoryWithoutEntityBase<ApplicationRoleGroup>
    {

    }
    public class ApplicationRoleGroupRepository : RepositoryBaseWithoutEntityBase<ApplicationRoleGroup>, IApplicationRoleGroupRepository
    {
        public ApplicationRoleGroupRepository(TaskSystemDbContext context) : base(context)
        {

        }
    }
}
