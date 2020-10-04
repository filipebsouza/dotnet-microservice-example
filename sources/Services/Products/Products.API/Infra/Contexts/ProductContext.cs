using Microsoft.EntityFrameworkCore;
using Products.API.Domain;

namespace Products.API.Infra.Contexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
    }
}