using Base.Domain;

namespace Products.API.Domain
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