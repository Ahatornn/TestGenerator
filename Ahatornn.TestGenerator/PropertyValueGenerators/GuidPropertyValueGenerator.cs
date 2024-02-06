using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Ahatornn.TestGenerator.PropertyValueGenerators
{
    internal class GuidPropertyValueGenerator : IPropertyValueGenerator
    {
        Type IPropertyValueGenerator.PropertyValueType => typeof(Guid);

        void IPropertyValueGenerator.Generate<TEntity>([NotNull] TEntity entity, [NotNull] PropertyInfo propertyInfo)
            where TEntity : class
        {
            if (!(propertyInfo.CanWrite && propertyInfo.PropertyType == typeof(Guid)))
            {
                throw new InvalidOperationException($"Свойство {propertyInfo.Name} не может быть записано для {nameof(GuidPropertyValueGenerator)}");
            }
            propertyInfo.SetValue(entity, Guid.NewGuid());
        }
    }
}
