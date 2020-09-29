using Bogus;

namespace Products.Tests.Domain
{
    public class ProductTests
    {
        private readonly Faker _faker;

        public ProductTests()
        {
            _faker = new Faker();
        }
    }
}