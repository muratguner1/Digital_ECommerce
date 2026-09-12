using Digital_Domain_Layer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Digital_Persistence_Layer.AppDbContext;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {}
    
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<MainCategory> MainCategories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public new DbSet<User> Users { get; set; } //new eklendi
    
}