using System.Reflection;

namespace Ahatornn.TestGenerator.PropertyValueGenerators
{
    internal class StringPropertyValueGenerator : BasePropertyValueGenerator<string>
    {
        protected override string GetPropertyValue(PropertyInfo propertyInfo) => $"{propertyInfo.Name}{Guid.NewGuid():N}";
    }
}
