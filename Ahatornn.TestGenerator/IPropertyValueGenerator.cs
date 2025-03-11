using System.Reflection;

namespace Ahatornn.TestGenerator
{
    /// <summary>
    /// Писатель значения свойства по типу
    /// </summary>
    public interface IPropertyValueGenerator
    {
        /// <summary>
        /// Тип свойства, для которого устанавливается <see cref="IPropertyValueGenerator"/>
        /// </summary>
        Type PropertyValueType { get; }

        /// <summary>
        /// Генерирует значение для свойства
        /// </summary>
        void Generate<TEntity>(TEntity entity, PropertyInfo propertyInfo) where TEntity : class;
    }
}
