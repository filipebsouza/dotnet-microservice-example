using System.Threading.Tasks;
using Products.API.Domain.Dtos;

namespace Products.API.Domain.Service
{
    public interface ISaveProductService
    {
        Task<ProductSavedDto> Save(ProductToSaveDto dto);
    }
}