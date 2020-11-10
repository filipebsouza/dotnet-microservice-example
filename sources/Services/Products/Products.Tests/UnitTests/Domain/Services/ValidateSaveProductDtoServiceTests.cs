using System.Threading.Tasks;
using Bogus;
using Common.Resources.Notifications.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using Products.API.Domain.Dtos;
using Products.API.Domain.Service;
using Xunit;

namespace Products.Tests.UnitTests.Domain.Services
{
    public class ValidateSaveProductDtoServiceTests
    {
        private readonly Faker _faker;
        private readonly Mock<ICommonNotificationContext> _notificationContextMock;
        private readonly Mock<IStringLocalizer> _stringLocalizerMock;
        private readonly ValidateSaveProductDtoService _validateSaveProductDtoService;

        public ValidateSaveProductDtoServiceTests()
        {
            _faker = new Faker();
            _notificationContextMock = new Mock<ICommonNotificationContext>();
            _stringLocalizerMock = new Mock<IStringLocalizer>();
            _validateSaveProductDtoService = new ValidateSaveProductDtoService(
                _notificationContextMock.Object,
                _stringLocalizerMock.Object
            );
        }

        [Fact]
        public async Task NotifyWhenProductDescriptionShouldBeInvalid()
        {
            //Given
            var invalidDescription = string.Empty;
            var dto = new ProductToSaveDto
            {
                Name = _faker.Name.FirstName(),
                Description = invalidDescription
            };
            var key = nameof(dto.Description);
            var mockedErrorMessage = "Description is invalid.";
            var localizedString = new LocalizedString(key, mockedErrorMessage);
            _stringLocalizerMock.Setup(_ => _[key]).Returns(localizedString);

            //When
            var ehValido = await _validateSaveProductDtoService.Validate(dto);

            //Then
            ehValido.Should().BeFalse();
            _notificationContextMock.Verify(n => n.AddNotification(key, mockedErrorMessage), Times.Once);
        }
    }
}