using Microsoft.EntityFrameworkCore;

public class OnlineShopDbContext :DbContext
{
    public OnlineShopDbContext(DbContextOptions options):base(options)
    {
    }

    //public DbSet<Product> Products {get; set;}
    public DbSet<Product> Products => Set<Product>();
}