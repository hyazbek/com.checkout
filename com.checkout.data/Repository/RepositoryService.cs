using com.checkout.data.Model;

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
        public int SaveChanges()
        {
            return repository._context.SaveChanges();
        }
    }
}