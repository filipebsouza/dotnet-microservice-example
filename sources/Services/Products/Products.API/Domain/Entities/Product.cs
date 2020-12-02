using Common.Domain.Entities;
using Products.API.Domain.Contracts;
using Products.API.Resources.Notifications.Interfaces;

namespace Products.API.Domain.Entities
{
    public class Product : EntityBase
    {
        protected Product() { }
        public Product(string name, string description, IProductNotifications notifications)
        {
            var contrato = new ProductContract(
                name,
                description,
                notifications
            );

            if (contrato.Valid)
            {
                Name = name;
                Description = description;
            }
            else
            {
                AddNotifications(contrato.Notifications);
            }
        }

        public string Name { get; }
        public string Description { get; }
        public decimal? Price { get; internal set; }
        public int? Classification { get; internal set; }
    }
}