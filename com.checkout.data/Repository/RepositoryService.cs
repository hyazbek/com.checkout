namespace com.checkout.data.Repository
{
    public class RepositoryService
    {
        private readonly EFRepository repository;

        public RepositoryService(EFRepository repository)
        {
            this.repository = repository;
        }

        public void Add<EntityType>(EntityType entity) => repository.Add(entity);
        public void Remove<EntityType>(EntityType entity) => repository.Remove(entity);
        
    }
}