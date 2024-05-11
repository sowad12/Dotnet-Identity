using IdentityManager.Library.Contexts;
using IdentityManager.Library.Models.Entites;
using Microsoft.AspNetCore.Identity;

namespace IdentityManager.Main.Extensions
{
    public static class IdentityConfigurationDependencies
    {
        public static IServiceCollection AddIdentityDependencies(this IServiceCollection services, IConfiguration _configuration)
        {
            //IdentityConfiguration
            services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(opt =>
            {
                //password configuration
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredUniqueChars = 0;

                //lockedout Configuration
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                opt.SignIn.RequireConfirmedEmail = false;

            });
            return services;
        }
    }
}
