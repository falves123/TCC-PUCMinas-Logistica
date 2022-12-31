namespace Tests_Application.Delivery;
public class DeliveryModelMock : Application.Services.Delivery.Model.Delivery
{
    public override Domain.Aggregates.Delivery.Delivery ToDomain()
    {
        var agg = MockDeliveryAggRoot(this.ID, this.Started, this.Finished, this.OrderID, this.Total);
        return agg;
    }

    public override DeliveryModelMock ToDelivery(Domain.Aggregates.Delivery.Delivery agg)
    {
        var model = new DeliveryModelMock();
        model.ID = agg.ID;
        model.Started = agg.Started;
        model.Finished = agg.Finished;
        model.OrderID = agg.OrderID;
        model.Total = agg.Total;
        return model;
    }

    public override Domain.Aggregates.Delivery.Delivery ToDomain(Guid id)
    {
        var agg = MockDeliveryAggRoot(Guid.Empty, DateTime.MinValue, DateTime.MinValue, Guid.Empty, 0);
        agg.DevPrimeSetProperty<Guid>("ID", id);
        return agg;
    }
    private Domain.Aggregates.Delivery.Delivery MockDeliveryAggRoot(Guid iD, DateTime started, DateTime finished, Guid orderID, double total)
    {
        var AggMock = new Mock<Domain.Aggregates.Delivery.Delivery>(iD, started, finished, orderID, total);
        var agg = AggMock.Object;
        AggMock.Setup(o => o.Add()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new DeliveryCreated());
        });
        AggMock.Setup(o => o.Update()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new DeliveryUpdated());
        });
        AggMock.Setup(o => o.Delete()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new DeliveryDeleted());
        });
        return agg;
    }
}
public class DeliveryServiceMock
{
    public List<DomainEvent> OutPutDomainEvents { get; set; }
    public Application.Services.Delivery.Model.Delivery MockCommand()
    {
        var agg = new DeliveryModelMock();
        agg.Started = DateTime.Now;
        agg.Finished = DateTime.Now;
        agg.Total = Faker.RandomNumber.Next();
        return agg;
    }

    public DeliveryService MockDeliveryService()
    {
        OutPutDomainEvents = new List<DomainEvent>();
        var state = new DeliveryStateMock();
        var dp = new DpMock<IDeliveryState>(state, (domainevent) =>
        {
            OutPutDomainEvents.Add(domainevent);
        });
        var srv = new DeliveryService(state, dp);
        return srv;
    }
}