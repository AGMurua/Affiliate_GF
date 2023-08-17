using AffiliatesApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AffiliatesApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AffiliateEntity> Affiliate { get; set; }
        public DbSet<CustomerEntity> Customer { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>()
            .HasOne(c => c.Affiliate)
            .WithMany(a => a.Customers)
            .HasForeignKey(c => c.AffiliateId);
        }
    }
}
