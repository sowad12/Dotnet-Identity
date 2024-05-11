

using IdentityManager.Library.Contexts;

namespace IdentityManager.Library.Seeders
{
    public class MainUpdater
    {
        public string Update(string connectionString)
        {
            var context = new ApplicationDbContextFactory().Create(connectionString);
            string msg = string.Empty;
            return msg;
        }
    }
}
