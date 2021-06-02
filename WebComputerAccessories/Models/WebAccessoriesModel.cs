using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebComputerAccessories.Models
{
    public partial class WebAccessoriesModel : DbContext
    {
        public WebAccessoriesModel()
            : base("name=WebAccessoriesEntities")
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.AppUser)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.AppUser)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.CartDetails)
                .WithOptional(e => e.Cart)
                .HasForeignKey(e => e.IdCart);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.IdCategory);

            modelBuilder.Entity<Order>()
                .Property(e => e.ShipPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithOptional(e => e.Order)
                .HasForeignKey(e => e.IdOrder);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.CartDetails)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.IdProduct);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.IdProduct);
        }
    }
}
