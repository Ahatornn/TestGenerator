# TestGenerator
Data generator for test projects

Every time you write tests, you have to initialize data to test specific cases of your services. To separate models with default values from prepared models, use a managed data generator. This will allow you to verify the correct behavior of the business logic when checking tests and get rid of the unwanted effects of default behavior for empty models in the test.

Use ```EntityProvider.Shared.Create<>```
for creating test entity. For example:
```cs
var actCarrier1 = EntityProvider.Shared.Create<ActCarrier>();
var actCarrierOrder1 = EntityProvider.Shared.Create<ActCarrierOrder>(x => x.ActCarrierId = actCarrier.Id);
var actCarrierOrder2 = EntityProvider.Shared.Create<ActCarrierOrder>(x => 
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

By default, EntityProvider generates data for the following property types (if they are writable)
| Type | Generated value |
|-------|----------|
| String | ```$"{propertyInfo.Name}{Guid.NewGuid():N}"``` |
| DateTime | ```DateTime.Now``` |
| DateTimeOffset | ```DateTimeOffset.Now``` |
| Guid | ```Guid.NewGuid()``` |

You can expand the list of available data generators by implementing the interface ```IPropertyValueGenerator```. However, in this case you will need to use the ```EntityProvider``` instance obtained using the ```GeneratorFactory```, rather than using ```EntityProvider.Shared```
