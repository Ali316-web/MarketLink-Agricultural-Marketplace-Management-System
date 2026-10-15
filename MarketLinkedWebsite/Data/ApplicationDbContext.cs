using LocalFarmersMarket.Entities;
using MarketLink.Admin.Models.Entities;
using MarketLink.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MarketLink.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets (Tables)
        public DbSet<FarmerProfile> FarmerProfiles { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<MarketFarmer> MarketFarmers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<FavoriteFarmer> FavoriteFarmers { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 1. Composite Key for MarketFarmer (M:N)
            builder.Entity<MarketFarmer>()
                .HasKey(mf => new { mf.MarketId, mf.FarmerProfileId });

            builder.Entity<MarketFarmer>()
                .HasOne(mf => mf.Market)
                .WithMany(m => m.MarketFarmers)
                .HasForeignKey(mf => mf.MarketId);

            builder.Entity<MarketFarmer>()
                .HasOne(mf => mf.FarmerProfile)
                .WithMany(f => f.MarketFarmers)
                .HasForeignKey(mf => mf.FarmerProfileId);

            // 2. Composite Key for FavoriteFarmer
            builder.Entity<FavoriteFarmer>()
                .HasKey(ff => new { ff.UserId, ff.FarmerProfileId });

            builder.Entity<FavoriteFarmer>()
                .HasOne(ff => ff.User)
                .WithMany(u => u.FavoriteFarmers)
                .HasForeignKey(ff => ff.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FavoriteFarmer>()
                .HasOne(ff => ff.FarmerProfile)
                .WithMany(f => f.FavoritedByUsers)
                .HasForeignKey(ff => ff.FarmerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. Composite Key for FavoriteProduct
            builder.Entity<FavoriteProduct>()
                .HasKey(fp => new { fp.UserId, fp.ProductId });

            builder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.User)
                .WithMany(u => u.FavoriteProducts)
                .HasForeignKey(fp => fp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.Product)
                .WithMany(p => p.FavoritedByUsers)
                .HasForeignKey(fp => fp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // 4. Order Relationships
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. Review Relationships
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}