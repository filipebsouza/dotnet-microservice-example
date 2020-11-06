using System.Linq;
using FluentAssertions;
using Bogus;
using Products.API.Domain.Entities;
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
            product.Valid.Should().BeTrue();
            productName.Should().BeEquivalentTo(product.Name);
        }

        [Fact]
        public void ProductNameShoulBeInvalid()
        {
            //Given
            var invalidProductName = (string)null;

            //When
            var product = new Product(
                invalidProductName,
                string.Concat(_faker.Lorem.Words(10))
            );

            //Then
            product.Should().BeNull();            
        }
    }
}