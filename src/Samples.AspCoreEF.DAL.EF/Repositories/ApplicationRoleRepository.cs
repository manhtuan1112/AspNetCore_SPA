using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;

namespace Samples.AspCoreEF.DAL.EF.Repositories
{
    public interface IApplicationRoleRepository : IRepositoryWithoutEntityBase<ApplicationRole>
    {
        IEnumerable<ApplicationRole> GetListRoleByGroupId(long groupId);
    }
    public class ApplicationRoleRepository : RepositoryBaseWithoutEntityBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(TaskSystemDbContext context) : base(context)
        {
        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(long groupId)
        {
            var query = from role in DbContext.ApplicationRoles
                        join rg in DbContext.ApplicationRoleGroups
                        on role.Id equals rg.RoleId
                        where rg.GroupId == groupId
                        select role;
            return query;
        }
    }
}
