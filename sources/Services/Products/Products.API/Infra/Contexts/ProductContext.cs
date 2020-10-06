using Microsoft.EntityFrameworkCore;
using Products.API.Domain;
using Products.API.Infra.Contexts.Mappings;

namespace Products.API.Infra.Contexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductMappingConfiguration());
        }

        public DbSet<Product> Products { get; set; }
    }
}