using System.Linq;
using FluentAssertions;
using Bogus;
using Products.API.Domain.Entities;
using Xunit;
using Moq;
using Products.API.Resources.Notifications.Interfaces;

namespace Products.Tests.UnitTests.Domain
{
    public class ProductTests
    {
        private readonly Faker _faker;
        private readonly Mock<IProductNotifications> _productNotificationsMock;

        public ProductTests()
        {
            _faker = new Faker();
            _productNotificationsMock = new Mock<IProductNotifications>();
        }

        [Fact]
        public void ProductNameShouldBeValid()
        {
            //Given
            var productName = _faker.Random.String();

            //When
            var product = new Product(
                productName,
                string.Concat(_faker.Lorem.Words(10)),
                _productNotificationsMock.Object
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
            _productNotificationsMock
                .Setup(_ => _.ProductNameNotBeInvalid)
                .Returns(invalidProductName);

            //When
            var product = new Product(
                invalidProductName,
                string.Concat(_faker.Lorem.Words(10)),
                _productNotificationsMock.Object
            );

            //Then
            product.Invalid.Should().BeTrue();
        }
    }
}