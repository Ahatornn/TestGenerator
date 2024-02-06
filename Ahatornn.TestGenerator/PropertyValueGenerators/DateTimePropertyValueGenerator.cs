using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Ahatornn.TestGenerator.PropertyValueGenerators
{
    internal class DateTimePropertyValueGenerator : IPropertyValueGenerator
    {
        Type IPropertyValueGenerator.PropertyValueType => typeof(DateTime);

        public void Generate<TEntity>([NotNull] TEntity entity, [NotNull] PropertyInfo propertyInfo)
            where TEntity : class
        {
            if (!(propertyInfo.CanWrite && propertyInfo.PropertyType == typeof(DateTime)))
            {
                throw new InvalidOperationException($"Свойство {propertyInfo.Name} не может быть записано для {nameof(DateTimePropertyValueGenerator)}");
            }

            propertyInfo.SetValue(entity, DateTime.Now);
        }
    }
}
