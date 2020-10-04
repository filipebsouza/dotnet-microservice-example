using System.Linq;
using Bogus;
using Products.API.Domain;
using Xunit;

namespace Products.Tests.Domain
{
    public class ProductTests
    {
        private readonly Faker _faker;

        public ProductTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void ProductNameShouldBeValid()
        {
            //Given
            var productName = _faker.Random.String();

            //When
            var product = new Product(
                productName,
                string.Concat(_faker.Lorem.Words(10))
            );

            //Then
            Assert.True(product.Valid);
            Assert.Equal(productName, product.Name);
        }
    }
}