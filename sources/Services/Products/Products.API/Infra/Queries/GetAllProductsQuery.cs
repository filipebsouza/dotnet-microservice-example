using System.Linq;
using Base.Infra.Queries.Specifications;
using Products.API.Domain;
using Products.API.Infra.Contexts;
using Products.API.Infra.Filters;
using Products.API.Resources.Notifications;
using Products.API.Infra.Queries.Filters;
using Products.API.Infra.Queries.Interfaces;
using Base.Resources.Notifications;

namespace Products.API.Infra.Queries
{
    public class GetAllProductsQuery : IGetAllProductsQuery
    {
        private readonly ProductContext _productContext;
        private readonly IProductNotifications _notifications;
        private readonly IBaseNotificationsContext _notificationsContext;

        public GetAllProductsQuery(
            ProductContext productContext,
            IProductNotifications notifications,
            IBaseNotificationsContext notificationsContext
        )
        {
            _productContext = productContext;
            _notifications = notifications;
            _notificationsContext = notificationsContext;
        }

        public IQueryable<Product> Get(ProductFilter filter)
        {
            _notificationsContext.AddNotification(this.GetType().Name, _notifications.ProductNameNotBeInvalid);

            return _productContext.Set<Product>()
                .Specify(new ProductContainsNameSpecification(filter.Name))
                .Specify(new ProductRangeByPriceSpecification(filter.MinPrice, filter.MaxPrice))
                .Specify(new PaginationSpecification<Product>());
        }
    }
}