using System.Linq;
using Common.Infra.Queries.Specifications;
using Products.API.Domain.Entities;
using Products.API.Infra.Contexts;
using Products.API.Infra.Filters;
using Products.API.Infra.Queries.Filters;
using Products.API.Infra.Queries.Interfaces;
using Products.API.Resources.Notifications.Interfaces;
using Common.Resources.Notifications.Interfaces;

namespace Products.API.Infra.Queries
{
    public class GetAllProductsQuery : IGetAllProductsQuery
    {
        private readonly ProductContext _productContext;
        private readonly IProductNotifications _notifications;
        private readonly ICommonNotificationContext _notificationContext;

        public GetAllProductsQuery(
            ProductContext productContext,
            IProductNotifications notifications,
            ICommonNotificationContext notificationContext
        )
        {
            _productContext = productContext;
            _notifications = notifications;
            _notificationContext = notificationContext;
        }

        public IQueryable<Product> Get(ProductFilter filter)
        {
            _notificationContext.AddNotification(this.GetType().Name, _notifications.ProductNameNotBeInvalid);

            return _productContext.Set<Product>()
                .Specify(new ProductContainsNameSpecification(filter.Name))
                .Specify(new ProductRangeByPriceSpecification(filter.MinPrice, filter.MaxPrice))
                .Specify(new PaginationSpecification<Product>());
        }
    }
}