using FluentAssertions;
using Xunit;

namespace Ahatornn.TestGenerator.Tests
{
    /// <summary>
    /// Тесты для <see cref="TestEntityProvider"/>
    /// </summary>
    public class TestEntityProviderTests
    {
        [Fact]
        public void SimpleGenerator()
        {
            //Arrange
            var testEntityProvider = new TestEntityProviderBuilder().Build();

            //Act
            var result = testEntityProvider.Create<SimpleTestModel>();

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    DependencyEntityId = Guid.Empty,
                    Description = (string?)null,
                    LastUpdatedAt = DateTime.MinValue,
                    DeletedAt = DateTimeOffset.MinValue,
                    Discount = (decimal?)null,
                    Cost = 0m,
                });
            result.Name.Should()
                .NotBeNull()
                .And.NotBeEmpty();
            result.Id.Should().NotBeEmpty();
            result.CreatedAt.Should().NotBe(DateTime.MinValue);
            result.CreatedAtDefault.Should().NotBe(DateTime.MinValue);
            result.ActualDate.Should().NotBe(DateTimeOffset.MinValue);
        }

        [Fact]
        public void ActionGenerator()
        {
            //Arrange
            var testEntityProvider = new TestEntityProviderBuilder().Build();
            var nameValue = $"{Guid.NewGuid():D}";
            var createdAtValue = new DateTime(2024, 2, 6);

            //Act
            var result = testEntityProvider.Create<SimpleTestModel>(x =>
            {
                x.Cost = 21m;
                x.Name = nameValue;
                x.CreatedAt = createdAtValue;
            });

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    DependencyEntityId = Guid.Empty,
                    Description = (string?)null,
                    LastUpdatedAt = DateTime.MinValue,
                    DeletedAt = DateTimeOffset.MinValue,
                    Discount = (decimal?)null,
                    Cost = 21m,
                });
            result.Name.Should().Be(nameValue);
            result.Id.Should().NotBeEmpty();
            result.CreatedAt.Should().Be(createdAtValue);
            result.CreatedAtDefault.Should().NotBe(DateTime.MinValue);
            result.ActualDate.Should().NotBe(DateTimeOffset.MinValue);
        }

        [Fact]
        public void PresetGenerator()
        {
            //Arrange
            var createdAtValue = new DateTime(2024, 2, 6);
            var testEntityProvider = new TestEntityProviderBuilder()
                .AddPreset<SimpleTestModel>(x =>
                {
                    x.Cost = 11.7m;
                    x.CreatedAtDefault = createdAtValue;
                })
                .Build();
            var nameValue = $"{Guid.NewGuid():D}";

            //Act
            var result = testEntityProvider.Create<SimpleTestModel>(x =>
            {
                x.Cost = 21m;
                x.Name = nameValue;
                x.CreatedAt = createdAtValue;
            });

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    DependencyEntityId = Guid.Empty,
                    Description = (string?)null,
                    LastUpdatedAt = DateTime.MinValue,
                    DeletedAt = DateTimeOffset.MinValue,
                    Discount = (decimal?)null,
                    Cost = 21m,
                });
            result.Name.Should().Be(nameValue);
            result.Id.Should().NotBeEmpty();
            result.CreatedAt.Should().Be(createdAtValue);
            result.CreatedAtDefault.Should().Be(createdAtValue);
            result.ActualDate.Should().NotBe(DateTimeOffset.MinValue);
        }

        [Fact]
        public void CustomPropertyValueGenerator()
        {
            //Arrange
            var testEntityProvider = new TestEntityProviderBuilder()
                .AddGenerator(new TestDecimalPropertyValueGenerator())
                .Build();

            //Act
            var result = testEntityProvider.Create<SimpleTestModel>();

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    Cost = TestDecimalPropertyValueGenerator.TestValue,
                });
        }
    }
}
