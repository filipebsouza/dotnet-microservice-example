using Common.Domain.Entities;

namespace Products.API.Domain.Entities
{
    public class Product : EntityBase
    {
        protected Product() { }
        public Product(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
        public decimal? Price { get; internal set; }
        public int? Classification { get; internal set; }
    }
}