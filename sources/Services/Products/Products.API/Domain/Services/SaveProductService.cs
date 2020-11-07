using System.Threading.Tasks;
using Base.Resources.Notifications;
using Products.API.Domain.Dtos;
using Products.API.Resources.Notifications;

namespace Products.API.Domain.Service
{
    public class SaveProductService : BaseNotificationsContext, ISaveProductService
    {
        private readonly IProductNotifications _notifications;

        public SaveProductService(
            IProductNotifications notifications
        )
        {
            _notifications = notifications;
        }

        public async Task<ProductSavedDto> Save(ProductToSaveDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}