namespace IdentityManager.Library.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}
