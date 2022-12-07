namespace Tests_Application.Product;
public class ProductModelMock : Application.Services.Product.Model.Product
{
    public override Domain.Aggregates.Product.Product ToDomain()
    {
        var agg = MockProductAggRoot(this.ID, this.Name, this.Description, this.Sku, this.Quantity);
        return agg;
    }

    public override ProductModelMock ToProduct(Domain.Aggregates.Product.Product agg)
    {
        var model = new ProductModelMock();
        model.ID = agg.ID;
        model.Name = agg.Name;
        model.Description = agg.Description;
        model.Sku = agg.Sku;
        model.Quantity = agg.Quantity;
        return model;
    }

    public override Domain.Aggregates.Product.Product ToDomain(Guid id)
    {
        var agg = MockProductAggRoot(Guid.Empty, String.Empty, String.Empty, String.Empty, 0);
        agg.DevPrimeSetProperty<Guid>("ID", id);
        return agg;
    }
    private Domain.Aggregates.Product.Product MockProductAggRoot(Guid iD, string name, string description, string sku, int quantity)
    {
        var AggMock = new Mock<Domain.Aggregates.Product.Product>(iD, name, description, sku, quantity);
        var agg = AggMock.Object;
        AggMock.Setup(o => o.Add()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new ProductCreated());
        });
        AggMock.Setup(o => o.Update()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new ProductUpdated());
        });
        AggMock.Setup(o => o.Delete()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new ProductDeleted());
        });
        return agg;
    }
}
public class ProductServiceMock
{
    public List<DomainEvent> OutPutDomainEvents { get; set; }
    public Application.Services.Product.Model.Product MockCommand()
    {
        var agg = new ProductModelMock();
        agg.Name = Faker.Lorem.Sentence();
        agg.Description = Faker.Lorem.Sentence();
        agg.Sku = Faker.Lorem.Sentence();
        agg.Quantity = Faker.RandomNumber.Next();
        return agg;
    }

    public ProductService MockProductService()
    {
        OutPutDomainEvents = new List<DomainEvent>();
        var state = new ProductStateMock();
        var dp = new DpMock<IProductState>(state, (domainevent) =>
        {
            OutPutDomainEvents.Add(domainevent);
        });
        var srv = new ProductService(state, dp);
        return srv;
    }
}