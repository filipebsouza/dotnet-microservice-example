using Microsoft.EntityFrameworkCore;
using Products.API.Domain;

namespace Products.API.Infra.Contexts
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}