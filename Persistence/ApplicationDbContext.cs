using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Product>? Product { get; set; }
        public DbSet<Domain.Entities.Cart>? Cart { get; set; }
        public DbSet<Domain.Entities.CartItem>? CartItem { get; set; }
        public DbSet<Domain.Entities.Category>? Category { get; set; }
        public DbSet<Domain.Entities.User>? User { get; set; }
        public DbSet<Domain.Entities.Order>? Order { get; set; }
        public DbSet<Domain.Entities.OrderItem>? OrderItem { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartItem>().HasKey(c => new { c.CartID, c.ProductID });
            modelBuilder.Entity<OrderItem>().HasKey(c => new { c.OrderID, c.ProductID });
        }

    }
}
