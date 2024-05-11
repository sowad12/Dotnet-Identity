

using IdentityManager.Library.Contexts;
using IdentityManager.Library.Infrastructure;
using IdentityManager.Library.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace IdentityManager.Main.Extensions
{
    public static class DatabaseContextExtensions
    {
        public static IServiceCollection AddDatabaseContextService(this IServiceCollection services, IConfiguration configuration)//, ILogger<Program> logger)
        {
            ILogger<Program> logger = services
                          .BuildServiceProvider()
                          .GetRequiredService<ILogger<Program>>();
            services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));

            DatabaseOptions databaseOptions = configuration.GetSection(nameof(DatabaseOptions))
                .Get(typeof(DatabaseOptions)) as DatabaseOptions;

            // Repository
            //services.AddScoped<IDapperContext, DapperContext>();
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.Configure<Library.Infrastructure.Options.AppOptions>(configuration.GetSection(nameof(Library.Infrastructure.Options.AppOptions)));


            // Database Context
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //var connectionBuilder = services.BuildServiceProvider().GetService<IDatabaseConnectionBuilderService>();
                //var connectionString = connectionBuilder.BuildAsync().Result;

                if (!string.IsNullOrEmpty(databaseOptions?.ConnectionString))
                {
                    options.UseSqlServer(databaseOptions.ConnectionString, sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                        sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
                    })
                    .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
                }
            });


            // CHECK IF CONNECTION AVAILABLE
            var dbBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(databaseOptions.ConnectionString);
            using (var context = new ApplicationDbContext(dbBuilder.Options))
            {
                try
                {
                    var pendingMigrations = context.Database.GetPendingMigrations();
                    if (pendingMigrations.Any())
                    {
                        context.Database.Migrate();
                    }
                }
                catch (Exception e) { logger.LogError("Database migration issue occurred! Error Message: " + e.Message + "Stack Trace: " + e.StackTrace); throw; }
            }

            return services;
        }
        public static IApplicationBuilder UseDatabaseContextService(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
