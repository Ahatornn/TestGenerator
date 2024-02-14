using Ahatornn.TestGenerator.PropertyValueGenerators;
using FluentAssertions;
using Xunit;

namespace Ahatornn.TestGenerator.Tests
{
    /// <summary>
    /// Тесты для <see cref="DateTimeOffsetPropertyValueGenerator"/>
    /// </summary>
    public class DateTimeOffsetPropertyValueGeneratorTests
    {
        private readonly IPropertyValueGenerator generator = new DateTimeOffsetPropertyValueGenerator();

        [Fact]
        public void ShouldReturnStringType()
        {
            //Act
            var result = generator.PropertyValueType;

            //Assert
            result.Should().Be(typeof(DateTimeOffset));
        }

        [Fact]
        public void ShouldThrowByCantWrite()
        {
            //Arrange
            var model = new SimpleTestModel();
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.DeletedAt));

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
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.ActualDate));

            //Act
            Action act = () => generator.Generate(model, propertyInfo);

            //Assert
            act.Should().NotThrow();
            model.ActualDate.Should()
                .NotBe(DateTimeOffset.MinValue);
            model.DeletedAt.Should()
                .Be(DateTimeOffset.MinValue);
        }
    }
}
