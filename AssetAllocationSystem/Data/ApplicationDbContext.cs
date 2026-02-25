using Microsoft.EntityFrameworkCore;

namespace AssetAllocationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetAssignment> AssetAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Unique Serial Number
            modelBuilder.Entity<Asset>()
                .HasIndex(a => a.SerialNumber)
                .IsUnique();

            // One Asset → One Assignment
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.AssetAssignment)
                .WithOne(a => a.Asset)
                .HasForeignKey<AssetAssignment>(aa => aa.AssetId);
        }
    }
}