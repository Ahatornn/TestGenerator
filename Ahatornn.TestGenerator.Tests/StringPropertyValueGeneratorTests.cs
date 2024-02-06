using Ahatornn.TestGenerator.PropertyValueGenerators;
using FluentAssertions;
using Xunit;

namespace Ahatornn.TestGenerator.Tests
{
    /// <summary>
    /// Тесты для <see cref="StringPropertyValueGenerator"/>
    /// </summary>
    public class StringPropertyValueGeneratorTests
    {
        private readonly IPropertyValueGenerator generator;

        public StringPropertyValueGeneratorTests()
        {
            generator = new StringPropertyValueGenerator();
        }

        [Fact]
        public void ShouldReturnStringType()
        {
            //Act
            var result = generator.PropertyValueType;

            //Assert
            result.Should().Be(typeof(string));
        }

        [Fact]
        public void ShouldThrowByCantWrite()
        {
            //Arrange
            var model = new SimpleTestModel();
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.Description));

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
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.DeletedAt));

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
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.Name));

            //Act
            Action act = () => generator.Generate(model, propertyInfo);

            //Assert
            act.Should().NotThrow();
            model.Name.Should()
                .NotBeEmpty()
                .And
                .StartWith(nameof(model.Name));
        }
    }
}
