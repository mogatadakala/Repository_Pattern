using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RepositoryApp.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 

        }
        public DbSet<Customers> customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>().ToTable("Customers").HasKey(e=> new
            {
                e.CustomerId
            });
            base.OnModelCreating(modelBuilder);
        }

        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }
    }
}
