using Ahatornn.TestGenerator.PropertyValueGenerators;

namespace Ahatornn.TestGenerator
{
    /// <summary>
    /// Поставщик сущностей
    /// </summary>
    public class TestEntityProvider
    {
        private readonly IDictionary<Type, object> presets = new Dictionary<Type, object>();
        private readonly IDictionary<Type, IPropertyValueGenerator> valueGenerators = new Dictionary<Type, IPropertyValueGenerator>();
        private readonly static Lazy<TestEntityProvider> sharedInstance = new(() => new TestEntityProvider());

        internal TestEntityProvider()
        {
            Initialize();
        }

        internal TestEntityProvider(IDictionary<Type, object> presets, IDictionary<Type, IPropertyValueGenerator> valueGenerators)
        {
            this.presets = presets;
            this.valueGenerators = valueGenerators;
            Initialize();
        }

        /// <summary>
        /// Предоставляет доступ к общему <see cref="TestEntityProvider"/>
        /// </summary>
        public static TestEntityProvider Shared { get; } = sharedInstance.Value;

        /// <summary>
        /// Создаёт сущность класса <see cref="TEntity"/>
        /// </summary>
        /// <typeparam name="TEntity">Создаваемая сущность</typeparam>
        /// <param name="settings">Действие, которое будет применено после создания сущности</param>
        /// <returns><see cref="TEntity"/></returns>
        public TEntity Create<TEntity>(Action<TEntity>? settings = null) where TEntity : class, new()
        {
            var entity = new TEntity();
            FillEntityProperties(entity);
            if (presets.TryGetValue(typeof(TEntity), out var preset))
            {
                if (preset is Action<TEntity> action)
                {
                    action.Invoke(entity);
                }
            }
            settings?.Invoke(entity);
            return entity;
        }

        private void Initialize()
        {
            valueGenerators.TryAdd(typeof(string), new StringPropertyValueGenerator());
            valueGenerators.TryAdd(typeof(DateTime), new DateTimePropertyValueGenerator());
            valueGenerators.TryAdd(typeof(DateTimeOffset), new DateTimeOffsetPropertyValueGenerator());
            valueGenerators.TryAdd(typeof(Guid), new GuidPropertyValueGenerator());
        }

        private void FillEntityProperties<TEntity>(TEntity entity)
            where TEntity : class
        {
            foreach (var writableProperty in entity.GetType().GetProperties()
                         .Where(x => x.CanWrite))
            {
                if (valueGenerators.TryGetValue(writableProperty.PropertyType, out var valueGenerator))
                {
                    valueGenerator.Generate(entity, writableProperty);
                }
            }
        }
    }
}
