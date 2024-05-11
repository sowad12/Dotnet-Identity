
using IdentityManager.Main.Extensions;
using Microsoft.Net.Http.Headers;


namespace IdentityManager.Main
{

    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            //services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddSession(options =>
            {
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(1440);
            });


            services.AddDependencies(_configuration);
            //services.AddControllers().AddNewtonsoftJson();
           

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
             IWebHostEnvironment env,
             //IRecurringJobManager recurringJobManager,
             IConfiguration configuration,
             IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();;
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseCors("corsapp");

            app.UseDependencies(configuration, serviceProvider);
            //app.UseDependencies(configuration, recurringJobManager, serviceProvider);

      

            app.UseRouting();

            app.UseAuthorization();

         
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });

            app.Use(async (httpContext, next) =>
            {
                httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate, max-age=0";
                httpContext.Response.Headers[HeaderNames.Expires] = "-1";
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
