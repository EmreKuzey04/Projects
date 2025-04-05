using Microsoft.EntityFrameworkCore;
using WEB.MVCUI.Models.Entities;

namespace WEB.MVCUI.Models.Contexts
{
    public class TradewndContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Tradewnd;Trusted_Connection=True;TrustServerCertificate=true");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }

    }
}
