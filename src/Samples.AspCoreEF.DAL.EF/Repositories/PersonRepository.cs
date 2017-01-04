using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;

namespace Samples.AspCoreEF.DAL.EF.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {

    }
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(TaskSystemDbContext context) : base(context)
        {
        }
    }
}
