using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Ahatornn.TestGenerator.PropertyValueGenerators
{
    internal abstract class BasePropertyValueGenerator<TType> : IPropertyValueGenerator
    {
        Type IPropertyValueGenerator.PropertyValueType => typeof(TType);

        void IPropertyValueGenerator.Generate<TEntity>([NotNull] TEntity entity, [NotNull] PropertyInfo propertyInfo)
            where TEntity : class
        {
            if (!(propertyInfo.CanWrite && propertyInfo.PropertyType == typeof(TType)))
            {
                throw new InvalidOperationException($"Свойство {propertyInfo.Name} не может быть записано для {GetType().Name}");
            }

            propertyInfo.SetValue(entity, GetPropertyValue(propertyInfo));
        }

        protected abstract TType GetPropertyValue(PropertyInfo propertyInfo);
    }
}
