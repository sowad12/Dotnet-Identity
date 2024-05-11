using IdentityManager.Library.Contexts;


namespace IdentityManager.Library.Seeders
{
    public interface ISeeder
    {
        void Seed(ApplicationDbContext context);
    }
}
