using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL
{
    public interface IRepository<Entity> : IDisposable where Entity : class
    {
        IEnumerable<Entity> GetAll();
        IEnumerable<Entity> GetPage(Expression<Func<Entity, bool>> predicate, int page, int pageSize);

        IEnumerable<Entity> Get(Expression<Func<Entity, bool>> predicate);

        Entity? FirstOrDefault(Expression<Func<Entity, bool>> predicate);

        int Count();

        void Add(Entity instance);
    }
}
