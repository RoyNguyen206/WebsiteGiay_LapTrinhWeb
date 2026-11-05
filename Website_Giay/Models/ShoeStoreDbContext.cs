using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Website_Giay.Models
{
    public class ShoeStoreDbContext : DbContext
    {
        public ShoeStoreDbContext() : base("name=ShoeStoreDbContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductColor> ProductColors { get; set; }
        public virtual DbSet<ProductSize> ProductSizes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Precision for decimals
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 0);
            modelBuilder.Entity<Product>().Property(p => p.OldPrice).HasPrecision(18, 0);
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 0);
            modelBuilder.Entity<OrderDetail>().Property(od => od.UnitPrice).HasPrecision(18, 0);

            // Avoid multiple cascade paths
            modelBuilder.Entity<Order>()
                .HasOptional(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .WillCascadeOnDelete(false);
        }
    }
}
