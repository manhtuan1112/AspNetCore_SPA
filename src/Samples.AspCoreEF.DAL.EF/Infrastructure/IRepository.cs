using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Samples.AspCoreEF.DAL.EF.Infrastructure
{
    public interface IRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();

        T Get(long id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}