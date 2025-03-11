# TestGenerator
Data generator for test projects

Every time you write tests, you have to initialize data to test specific cases of your services. To separate models with default values from prepared models, use a managed data generator. This will allow you to verify the correct behavior of the business logic when checking tests and get rid of the unwanted effects of default behavior for empty models in the test.

Use ```TestEntityProvider.Shared.Create<>```
for creating test entity. For example:
```cs
var actCarrier1 = TestEntityProvider.Shared.Create<ActCarrier>();
var actCarrierOrder1 = TestEntityProvider.Shared.Create<ActCarrierOrder>(x => x.ActCarrierId = actCarrier.Id);
var actCarrierOrder2 = TestEntityProvider.Shared.Create<ActCarrierOrder>(x => 
{
  x.ActCarrierId = actCarrierDeleted.Id;
  x.ExternalOrderId = orderId;
});
```
Method ```Create<>``` using instead
```cs
var actCarrier1 = new ActCarrier
{
    Id = Guid.NewGuid(),
    Currency = $"Currency{Guid.NewGuid():N}",
    DocumentDate = DateTimeOffset.UtcNow,
    DocumentNumber = $"DocumentNumber{Guid.NewGuid():N}",
    ExternalContractId = Guid.NewGuid(),
    ExternalContractorId = Guid.NewGuid(),
    ExternalId = Guid.NewGuid(),
    SourceUpdatedAt = DateTimeOffset.UtcNow,
};
```

By default, TestEntityProvider generates data for the following property types (if they are writable)
- String: ```$"{propertyInfo.Name}{Guid.NewGuid():N}"```
- DateTime: ```DateTime.Now```
- DateTimeOffset: ```DateTimeOffset.Now```
- Guid: ```Guid.NewGuid()```

You can expand the list of available data generators by implementing the interface ```IPropertyValueGenerator```. However, in this case you will need to use the ```TestEntityProvider``` instance obtained using the ```TestGeneratorFactory```, rather than using ```TestEntityProvider.Shared```.
Let's see how to implement a data generator for all ```boolean``` types.

First, let's implement IPropertyValueGenerator for the ```boolean``` type
```cs
public class BoolAlwaysTrueValueGenerator : IPropertyValueGenerator
{
  Type IPropertyValueGenerator.PropertyValueType => typeof(bool);

  void IPropertyValueGenerator.Generate<TEntity>([NotNull] TEntity entity, [NotNull] PropertyInfo propertyInfo)
    where TEntity : class
  {
    if (!(propertyInfo.CanWrite && propertyInfo.PropertyType == typeof(bool)))
    {
      throw new InvalidOperationException($"The property {propertyInfo.Name} could not be write for {GetType().Name}");
    }

    propertyInfo.SetValue(entity, true);
  }
}
```

Now let's create an ```TestEntityProviderBuilder``` by passing a custom IPropertyValueGenerator
```cs
var provider = new TestEntityProviderBuilder
    .AddGenerator(new BoolAlwaysTrueValueGenerator())
    .Build();
```

And finally use ```provider``` to generate data
```cs
var result = provider.Create<SimpleTestModel>();
```

If you regularly need to set the same values for certain properties when using ```TestEntityProvider```, use the ```AddPreset``` mechanism. Just specify the necessary values for properties of a certain type when creating the ```TestEntityProvider```
```cs
var provider = new TestEntityProviderBuilder
  .AddPreset<SimpleTestModel>(x => x.Cost = 11.7m)
  .Build();
```
Now every time you call ```provider.Create<SimpleTestModel>()``` you will receive a value of ```11.7m``` for the ```Cost``` property of each model obtained using ```TestEntityProvider```. Of course, if necessary, you can override the previously specified value ```provider.Create<SimpleTestModel>(x => x.Cost = 8.3m)```
