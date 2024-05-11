

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace IdentityManager.Library.Contexts
{
  
    public class ApplicationDbContextFactory
    {
        public ApplicationDbContext Create(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(
                connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                    sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
                })
                .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
