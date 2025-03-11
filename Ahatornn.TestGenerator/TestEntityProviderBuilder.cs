namespace Ahatornn.TestGenerator;

/// <summary>
/// Построитель для <see cref="TestEntityProvider"/>
/// </summary>
public class TestEntityProviderBuilder
{
    private readonly IDictionary<Type, object> presets = new Dictionary<Type, object>();
    private readonly IDictionary<Type, IPropertyValueGenerator> valueGenerators = new Dictionary<Type, IPropertyValueGenerator>();

    /// <summary>
    /// Добавляет обработчик действий при создании сущности 
    /// </summary>
    /// <typeparam name="TEntity">Тип создаваемой сущности</typeparam>
    /// <param name="entitySettings">Действия, которые будут применены после создания сущности</param>
    /// <returns><see cref="TestEntityProviderBuilder"/></returns>
    public TestEntityProviderBuilder AddPreset<TEntity>(params Action<TEntity>[] entitySettings)
        where TEntity : class
    {
        foreach (var entityAction in entitySettings)
        {
            presets.TryAdd(typeof(TEntity), entityAction);
        }

        return this;
    }

    /// <summary>
    /// Добавляет генератор данных для создания значений указанного типа
    /// </summary>
    /// <param name="generators">Список <see cref="IPropertyValueGenerator"/></param>
    /// <returns><see cref="TestEntityProviderBuilder"/></returns>
    public TestEntityProviderBuilder AddGenerator(params IPropertyValueGenerator[] generators)
    {
        foreach (var generator in generators)
        {
            valueGenerators.TryAdd(generator.PropertyValueType, generator);
        }

        return this;
    }

    /// <summary>
    /// Создаёт экземпляр <see cref="TestEntityProvider"/>
    /// </summary>
    public TestEntityProvider Build() => new TestEntityProvider(presets, valueGenerators);
}
