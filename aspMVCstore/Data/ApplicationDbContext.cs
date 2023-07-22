using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using aspMVCstore.Models;

namespace aspMVCstore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<aspMVCstore.Models.Product>? Product { get; set; }
        public DbSet<aspMVCstore.Models.Cart>? Cart { get; set; }
        public DbSet<aspMVCstore.Models.CartItem>? CartItem { get; set; }
        public DbSet<aspMVCstore.Models.User>? User { get; set; }
        public DbSet<aspMVCstore.Models.Order>? Order { get; set; }
        public DbSet<aspMVCstore.Models.OrderItem>? OrderItem { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartItem>().HasKey(c => new { c.CartID, c.ProductID });
            modelBuilder.Entity<OrderItem>().HasKey(c => new { c.OrderID, c.ProductID });
        }

    }
}  