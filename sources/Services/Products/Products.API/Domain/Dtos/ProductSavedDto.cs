using System;

namespace Products.API.Domain.Dtos
{
    public class ProductSavedDto : ProductBaseDto
    {
        public Guid Id { get; set; }
    }
}