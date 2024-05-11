


namespace IdentityManager.Main.Extensions
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ILogger<Program> logger = services
                           .BuildServiceProvider()
                           .GetRequiredService<ILogger<Program>>();

            //services.AddServices(new Assembly[] { Assembly.GetExecutingAssembly(), typeof(IProductManager).Assembly });

            //services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddHttpClient();
            // Options
            services.AddOptions();

            // Swagger
            //services.AddSwaggerService(configuration);

            // Add Database Context
            services.AddDatabaseContextService(configuration);
            
            services.AddScoped<ISession>(provider =>
            provider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext
                .Session);

            services.AddIdentityDependencies(configuration);
            services.Configure<Library.Infrastructure.Options.AppOptions>(configuration.GetSection(nameof(Library.Infrastructure.Options.AppOptions)));
           
            return services;
        }

        public static IApplicationBuilder UseDependencies(this IApplicationBuilder app, IConfiguration configuration,
           //IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider
        )
        {
            //app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseDefaultFiles();

            // Swagger
            //app.UseSwaggerService();
            //app.UseSwagger();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
