using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Persistence.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            provider.GetService<AppDbContext>().Database.Migrate();
        }

        public static Task SeedData(this IServiceProvider provider)
        {
            return DbInitializer.Seed(provider);
        }
    }
}
