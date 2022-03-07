using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Neetechs.Model;
using Neetechs_MVC.Models;

namespace Neetechs_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Neetechs_MVC.Models.Category> Category { get; set; }
        public DbSet<Neetechs_MVC.Models.Order> Order { get; set; }
        public DbSet<Neetechs_MVC.Models.Report> Report { get; set; }
        public DbSet<Neetechs_MVC.Models.CartItem> CartItem { get; set; }
    }
}