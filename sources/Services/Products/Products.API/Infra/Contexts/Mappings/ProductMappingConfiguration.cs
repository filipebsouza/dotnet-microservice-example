using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.API.Domain;

namespace Products.API.Infra.Contexts.Mappings
{
    public class ProductMappingConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.Notifications);
        }
    }
}