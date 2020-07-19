using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Template.Persistence;
using Template.Persistence.Entities;
using Template.Server.Execution;
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

        public static void AddIdentityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var passwordOptions = configuration.GetSection("PasswordOptions").Get<PasswordOptions>();

            services.AddIdentityCore<AppUser>(setup =>
            {
                setup.Password = passwordOptions;
                setup.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            var mailOptions = configuration.GetSection("MailKitOptions").Get<MailKitOptions>();
            services.AddMailKit(config =>
            {
                config.UseMailKit(mailOptions);
            });
        }

        public static void AddEtoshaServices(this IServiceCollection services)
        {
            services.AddScoped<IActionExecutor, ActionExecutor>();
            services.AddScoped<IAuthenticationProvider, AuthenticationProvider>();
        }
    }
}
