using System.Reflection;

namespace Ahatornn.TestGenerator.PropertyValueGenerators
{
    internal class GuidPropertyValueGenerator : BasePropertyValueGenerator<Guid>
    {
        protected override Guid GetPropertyValue(PropertyInfo propertyInfo) => Guid.NewGuid();
    }
}
