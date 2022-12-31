namespace Tests_Domain.Delivery;
public partial class DeliveryAggRootTest
{
    public Domain.Aggregates.Delivery.Delivery MockDelivery()
    {
        var agg = new Domain.Aggregates.Delivery.Delivery(Guid.Empty, DateTime.MinValue, DateTime.MinValue, Guid.Empty, 0);
        agg.Dp = new DpDomainMock(null);
        agg.DevPrimeSetProperty<Guid>("ID", Guid.NewGuid());
        agg.DevPrimeSetProperty<DateTime>("Started", DateTime.Now);
        agg.DevPrimeSetProperty<DateTime>("Finished", DateTime.Now);
        agg.DevPrimeSetProperty<Guid>("OrderID", Guid.NewGuid());
        agg.DevPrimeSetProperty<Double>("Total", Faker.RandomNumber.Next());
        agg.Dp.Source = agg;
        return agg;
    }
}