using System.Reflection;

namespace Ahatornn.TestGenerator.PropertyValueGenerators
{
    internal class DateTimePropertyValueGenerator : BasePropertyValueGenerator<DateTime>
    {
        protected override DateTime GetPropertyValue(PropertyInfo propertyInfo) => DateTime.Now;
    }
}
