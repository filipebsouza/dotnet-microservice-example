using System.Threading.Tasks;
using Common.Resources.Notifications.Interfaces;
using Products.API.Domain.Service.Interfaces;
using Products.API.Domain.Dtos;
using Products.API.Resources.Notifications.Interfaces;

namespace Products.API.Domain.Service
{
    public class SaveProductService : ISaveProductService
    {
        private readonly IProductNotifications _notifications;

        public SaveProductService(IProductNotifications notifications)
        {
            _notifications = notifications;
        }

        public async Task<ProductSavedDto> Save(ProductToSaveDto dto)
        {
            return await Task.Run<ProductSavedDto>(() =>
            {
                return new ProductSavedDto
                {
                    Name = "Mocked Saved Name",
                    Description = "Mocked Saved Description"
                };
            });
        }
    }
}