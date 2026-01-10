using ibop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ibop.Api.Data
{
    public class IbopDbContext : DbContext
    {
        public IbopDbContext(DbContextOptions<IbopDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW()");
        }
    }
}
