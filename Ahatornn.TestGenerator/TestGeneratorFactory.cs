namespace Ahatornn.TestGenerator
{
    /// <summary>
    /// Фабрика создания <see cref="TestEntityProvider"/>
    /// </summary>
    public class TestGeneratorFactory
    {
        /// <summary>
        /// Создаёт новый экземпляр <see cref="TestEntityProvider"/>
        /// </summary>
        public static TestEntityProvider Create() => new();

        /// <summary>
        /// Создаёт новый экземпляр <see cref="TestEntityProvider"/> указывая список <see cref="IPropertyValueGenerator"/>
        /// </summary>
        public static TestEntityProvider Create(params IPropertyValueGenerator[] generators) => new(generators);
    }
}
