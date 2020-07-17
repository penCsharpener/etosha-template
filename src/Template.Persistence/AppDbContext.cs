using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Persistence.Configurations;
using Template.Persistence.Entities;

namespace Template.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public override int SaveChanges()
        {
            var addedEntries = ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Added);

            foreach (var item in addedEntries)
            {
                item.Property(nameof(BaseEntity.CreationDate)).CurrentValue = DateTime.Now;
                item.Property(nameof(BaseEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }

            var modifiedEntries = ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Modified);

            foreach (var item in modifiedEntries)
            {
                item.Property(nameof(BaseEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppUserTypeConfiguration());
            builder.ApplyConfiguration(new AppRoleTypeConfiguration());
        }
    }
}
