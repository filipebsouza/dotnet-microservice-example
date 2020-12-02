using Flunt.Validations;
using Products.API.Resources.Notifications.Interfaces;

namespace Products.API.Domain.Contracts
{
    public class ProductContract : Contract
    {
        private readonly IProductNotifications _notifications;

        public ProductContract(string name, string description, IProductNotifications notifications)
        {
            _notifications = notifications;
            
            this.IsNotNullOrWhiteSpace(
                name,
                nameof(name),
                _notifications.ProductNameNotBeInvalid
            );

            this.IsNotNullOrWhiteSpace(
                description,
                nameof(description),
                _notifications.ProductNameNotBeInvalid
            );
        }
    }
}