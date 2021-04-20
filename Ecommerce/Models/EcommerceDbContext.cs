using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class EcommerceDbContext : DbContext
    {

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal)))
                property.SetColumnType("decimal(18,2)");
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ImagePath> ImagePaths { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }

        public static implicit operator EcommerceDbContext(SqlConnection v)
        {
            throw new NotImplementedException();
        }
    }
}
