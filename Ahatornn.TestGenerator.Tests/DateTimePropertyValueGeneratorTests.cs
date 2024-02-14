using Ahatornn.TestGenerator.PropertyValueGenerators;
using FluentAssertions;
using Xunit;

namespace Ahatornn.TestGenerator.Tests
{
    /// <summary>
    /// Тесты для <see cref="DateTimePropertyValueGenerator"/>
    /// </summary>
    public class DateTimePropertyValueGeneratorTests
    {
        private readonly IPropertyValueGenerator generator = new DateTimePropertyValueGenerator();

        [Fact]
        public void ShouldReturnStringType()
        {
            //Act
            var result = generator.PropertyValueType;

            //Assert
            result.Should().Be(typeof(DateTime));
        }

        [Fact]
        public void ShouldThrowByCantWrite()
        {
            //Arrange
            var model = new SimpleTestModel();
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.LastUpdatedAt));

            //Act
            Action act = () => generator.Generate(model, propertyInfo);

            //Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ShouldThrowByType()
        {
            //Arrange
            var model = new SimpleTestModel();
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.Id));

            //Act
            Action act = () => generator.Generate(model, propertyInfo);

            //Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ShouldWork()
        {
            //Arrange
            var model = new SimpleTestModel();
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.CreatedAt));

            //Act
            Action act = () => generator.Generate(model, propertyInfo);

            //Assert
            act.Should().NotThrow();
            model.CreatedAt.Should()
                .NotBe(DateTime.MinValue);
            model.CreatedAtDefault.Should()
                .Be(DateTime.MinValue);
        }
    }
}
