using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Template.Persistence;
using Template.Persistence.Entities;
using Template.Server.Providers.Implementations;
using Template.Server.Providers.Interfaces;

namespace Template.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        }

        public static void AddIdentityFramework(this IServiceCollection services, Microsoft.AspNetCore.Identity.PasswordOptions options)
        {
            services.AddIdentityCore<AppUser>(setup =>
            {
                setup.Password = options;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void AddEtoshaServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationProvider, AuthenticationProvider>();
        }
    }
}
