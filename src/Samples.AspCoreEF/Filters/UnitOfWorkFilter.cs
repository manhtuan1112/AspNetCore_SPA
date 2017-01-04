using Microsoft.AspNetCore.Mvc.Filters;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Filters
{
    public class UnitOfWorkFilter : ActionFilterAttribute
    {
        private readonly TaskSystemDbContext _dbContext;

        public UnitOfWorkFilter(TaskSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase))
                return;
            if (context.Exception == null && context.ModelState.IsValid)
            {
                _dbContext.Database.CommitTransaction();
            }
            else
            {
                _dbContext.Database.RollbackTransaction();
            }
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase))
                return;
            _dbContext.Database.BeginTransaction();
        }
    }

}
