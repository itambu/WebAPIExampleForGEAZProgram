using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.DAL.ByEF
{
    public class EFRepository<Entity> : IRepository<Entity> where Entity : class
    {
        private bool _disposed = false;

        private DbContext _dbContext;
        public EFRepository(DbContext dbContext)
        {
            CheckDisposeStatus();
            _dbContext = dbContext;
        }

        public void Add(Entity instance)
        {
            CheckDisposeStatus();
            _dbContext.Set<Entity>().Add(instance);
        }

        public IEnumerable<Entity> GetPage(Expression<Func<Entity, bool>> predicate, int page, int pageSize)
        {
            CheckDisposeStatus();
            return _dbContext.Set<Entity>()
                .Where(predicate)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToArray(); // query instantiation
        }

        private void CheckDisposeStatus()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Repository is disposed");
            }
        }

        public Entity? FirstOrDefault(Expression<Func<Entity, bool>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            CheckDisposeStatus();

            try
            {
                return _dbContext.Set<Entity>().FirstOrDefault(predicate);
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException("",ex);
            }
        }

        public IEnumerable<Entity> GetAll()
        {
            CheckDisposeStatus();
            return _dbContext.Set<Entity>().AsEnumerable();
        }

        public int Count()
        {
            CheckDisposeStatus();
            return _dbContext.Set<Entity>().Count();
        }

        public IEnumerable<Entity> Get(Expression<Func<Entity, bool>> predicate)
        {
            CheckDisposeStatus();
            return _dbContext.Set<Entity>().Where(predicate).ToArray();
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                    
                }
                _disposed = true;
            }
        }

        ~EFRepository() 
        {
            Dispose(false);

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}