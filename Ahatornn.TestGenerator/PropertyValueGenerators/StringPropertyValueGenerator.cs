using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Ahatornn.TestGenerator.PropertyValueGenerators
{
    internal class StringPropertyValueGenerator: IPropertyValueGenerator
    {
        Type IPropertyValueGenerator.PropertyValueType => typeof(string);

        void IPropertyValueGenerator.Generate<TEntity>([NotNull] TEntity entity, [NotNull] PropertyInfo propertyInfo)
            where TEntity : class
        {
            if (!(propertyInfo.CanWrite && propertyInfo.PropertyType == typeof(string)))
            {
                throw new InvalidOperationException($"Свойство {propertyInfo.Name} не может быть записано для {nameof(StringPropertyValueGenerator)}");
            }
            propertyInfo.SetValue(entity, $"{propertyInfo.Name}{Guid.NewGuid():N}");
        }


    }
}
