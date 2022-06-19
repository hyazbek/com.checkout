using com.checkout.data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace com.checkout.data.Repository
{
    public class EFRepository
    {
        private readonly CKODBContext _context;

        public EFRepository(CKODBContext context)
        {
            _context = context;
        }

        public void Add<EntityType>(EntityType? entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }
        public IQueryable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _context.Set<TEntity>().Where(predicate);
        }
        public bool Update<TEntity>(TEntity item) where TEntity : class
        {
            _context.Attach(item);
            _context.Update(item);
            _context.SaveChanges();
            return true;
        }

    }
}
