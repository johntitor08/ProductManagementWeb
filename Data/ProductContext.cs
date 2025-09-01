using Microsoft.EntityFrameworkCore;
using ProductManagementWeb.Models;

namespace ProductManagementWeb.Data
{
    public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
