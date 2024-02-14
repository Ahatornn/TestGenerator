using System.Reflection;

namespace Ahatornn.TestGenerator.PropertyValueGenerators
{
    internal class DateTimeOffsetPropertyValueGenerator : BasePropertyValueGenerator<DateTimeOffset>
    {
        protected override DateTimeOffset GetPropertyValue(PropertyInfo propertyInfo) => DateTimeOffset.Now;
    }
}
