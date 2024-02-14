using Ahatornn.TestGenerator.PropertyValueGenerators;
using FluentAssertions;
using Xunit;

namespace Ahatornn.TestGenerator.Tests
{
    /// <summary>
    /// Тесты для <see cref="GuidPropertyValueGenerator"/>
    /// </summary>
    public class GuidPropertyValueGeneratorTests
    {
        private readonly IPropertyValueGenerator generator = new GuidPropertyValueGenerator();

        [Fact]
        public void ShouldReturnStringType()
        {
            //Act
            var result = generator.PropertyValueType;

            //Assert
            result.Should().Be(typeof(Guid));
        }

        [Fact]
        public void ShouldThrowByCantWrite()
        {
            //Arrange
            var model = new SimpleTestModel();
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.DependencyEntityId));

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
            var propertyInfo = model.GetType().GetProperties().First(x => x.Name == nameof(model.Id));

            //Act
            Action act = () => generator.Generate(model, propertyInfo);

            //Assert
            act.Should().NotThrow();
            model.Id.Should().NotBeEmpty();
        }
    }
}
