using System.Reflection;

namespace Ahatornn.TestGenerator.PropertyValueGenerators
{
    internal class DateTimeOffsetPropertyValueGenerator : IPropertyValueGenerator
    {
        Type IPropertyValueGenerator.PropertyValueType => typeof(DateTimeOffset);

        public void Generate<TEntity>(TEntity entity, PropertyInfo propertyInfo)
            where TEntity : class
        {
            if (!(propertyInfo.CanWrite && propertyInfo.PropertyType == typeof(DateTimeOffset)))
            {
                throw new InvalidOperationException($"Свойство {propertyInfo.Name} не может быть записано для {nameof(DateTimeOffsetPropertyValueGenerator)}");
            }

            propertyInfo.SetValue(entity, DateTimeOffset.Now);
        }
    }
}
