using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Products.API.Domain.Dtos;
using Products.API.Domain.Service;
using Xunit;

namespace Products.Tests.UnitTests.Domain.Services
{
    public class ValidateSaveProductDtoServiceTests
    {
        private readonly Faker _faker;
        private readonly ValidateSaveProductDtoService _validateSaveProductDtoService;

        public ValidateSaveProductDtoServiceTests()
        {
            _faker = new Faker();
            _validateSaveProductDtoService = new ValidateSaveProductDtoService();
        }

        [Fact]
        public async Task TestName()
        {
            //Given
            var dto = new ProductToSaveDto
            {
                Name = _faker.Name.FirstName(),
                Description = _faker.Name.LastName()
            };

            //When
            var ehValido = await _validateSaveProductDtoService.Validate(dto);

            //Then
            ehValido.Should().BeTrue();
        }
    }
}