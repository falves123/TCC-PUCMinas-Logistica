namespace Tests_Application.Order;
public class OrderModelMock : Application.Services.Order.Model.Order
{
    public override Domain.Aggregates.Order.Order ToDomain()
    {
        var agg = MockOrderAggRoot(this.ID, this.CustomerName, this.CustomerTaxID, this.CustomerTaxID, this.Zipcode, this.Address, this.Number, this.City, this.UF, this.Complement, null, this.Total);
        return agg;
    }

    public override OrderModelMock ToOrder(Domain.Aggregates.Order.Order agg)
    {
        var model = new OrderModelMock();
        model.ID = agg.ID;
        model.CustomerName = agg.CustomerName;
        model.CustomerTaxID = agg.CustomerTaxID;
        model.Zipcode = agg.Zipcode;
        model.Address = agg.Address;
        model.Number = agg.Number;
        model.City = agg.City;
        model.UF = agg.UF;
        model.Complement = agg.Complement;
        model.Total = agg.Total;
        return model;
    }

    public override Domain.Aggregates.Order.Order ToDomain(Guid id)
    {
        var agg = MockOrderAggRoot(Guid.Empty, String.Empty, String.Empty, new List<Domain.Aggregates.Order.Item>(), 0);
        agg.DevPrimeSetProperty<Guid>("ID", id);
        return agg;
    }
    private Domain.Aggregates.Order.Order MockOrderAggRoot(Guid iD, string customerName, string customerTaxID, string Zipcode, string Address, string Number, string City, string UF, string Complement, List<Domain.Aggregates.Order.Item> items, double total)
    {
        var AggMock = new Mock<Domain.Aggregates.Order.Order>(iD, customerName, customerTaxID, Zipcode, Address, Number, City, UF, Complement, items, total);
        var agg = AggMock.Object;
        AggMock.Setup(o => o.Add()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new OrderCreated());
        });
        AggMock.Setup(o => o.Update()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new OrderUpdated());
        });
        AggMock.Setup(o => o.Delete()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new OrderDeleted());
        });
        return agg;
    }
}
public class OrderServiceMock
{
    public List<DomainEvent> OutPutDomainEvents { get; set; }
    public Application.Services.Order.Model.Order MockCommand()
    {
        var agg = new OrderModelMock();
        agg.CustomerName = Faker.Lorem.Sentence();
        agg.CustomerTaxID = Faker.Lorem.Sentence();
        agg.Zipcode = Faker.Lorem.Sentence();
        agg.Address = Faker.Lorem.Sentence();
        agg.Number = Faker.Lorem.Sentence();
        agg.City = Faker.Lorem.Sentence();
        agg.UF = Faker.Lorem.Sentence();
        agg.Complement = Faker.Lorem.Sentence();
        agg.Total = Faker.RandomNumber.Next();
        return agg;
    }

    public OrderService MockOrderService()
    {
        OutPutDomainEvents = new List<DomainEvent>();
        var state = new OrderStateMock();
        var dp = new DpMock<IOrderState>(state, (domainevent) =>
        {
            OutPutDomainEvents.Add(domainevent);
        });
        var srv = new OrderService(state, dp);
        return srv;
    }
}