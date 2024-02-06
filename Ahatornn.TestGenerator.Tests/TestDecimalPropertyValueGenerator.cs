using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Ahatornn.TestGenerator.Tests
{
    internal class TestDecimalPropertyValueGenerator: IPropertyValueGenerator
    {
        internal const decimal TestValue = -100m;
        Type IPropertyValueGenerator.PropertyValueType => typeof(decimal);

        public void Generate<TEntity>([NotNull] TEntity entity, [NotNull] PropertyInfo propertyInfo)
            where TEntity : class
        {
            propertyInfo.SetValue(entity, TestValue);
        }
    }
}
