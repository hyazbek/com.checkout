using com.checkout.data.Model;
using System.Linq.Expressions;

namespace com.checkout.data.Repository
{
    public class RepositoryService
    {
        private readonly EFRepository repository;

        public RepositoryService(EFRepository repository)
        {
            this.repository = repository;
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return repository.GetAll<TEntity>();
        }
        public TEntity Add<TEntity>(TEntity item) where TEntity : class
        {
            repository.Add(item);
            
            SaveChanges();
            
            return item;
        }
        public IQueryable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return repository
                .Find(predicate);
        }

        public int SaveChanges()
        {
            return repository._context.SaveChanges();
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            return repository.UpdateTransaction(transaction);
        }
    }
}