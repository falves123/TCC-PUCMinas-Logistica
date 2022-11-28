namespace Tests_Application.Customer;
public class CustomerModelMock : Application.Services.Customer.Model.Customer
{
    public override Domain.Aggregates.Customer.Customer ToDomain()
    {
        var agg = MockCustomerAggRoot(this.ID, this.Name, this.CPF, this.phone, this.email);
        return agg;
    }

    public override CustomerModelMock ToCustomer(Domain.Aggregates.Customer.Customer agg)
    {
        var model = new CustomerModelMock();
        model.ID = agg.ID;
        model.Name = agg.Name;
        model.CPF = agg.CPF;
        model.phone = agg.Phone;
        model.email = agg.Email;
        return model;
    }

    public override Domain.Aggregates.Customer.Customer ToDomain(Guid id)
    {
        var agg = MockCustomerAggRoot(Guid.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
        agg.DevPrimeSetProperty<Guid>("ID", id);
        return agg;
    }
    private Domain.Aggregates.Customer.Customer MockCustomerAggRoot(Guid iD, string name, string cpf, string Phone, string email)
    {
        var AggMock = new Mock<Domain.Aggregates.Customer.Customer>(iD, name, cpf, phone, email);
        var agg = AggMock.Object;
        AggMock.Setup(o => o.Add()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new CustomerCreated());
        });
        AggMock.Setup(o => o.Update()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new CustomerUpdated());
        });
        AggMock.Setup(o => o.Delete()).Callback(() =>
        {
            agg.Dp.ProcessEvent(new CustomerDeleted());
        });
        return agg;
    }
}
public class CustomerServiceMock
{
    public List<DomainEvent> OutPutDomainEvents { get; set; }
    public Application.Services.Customer.Model.Customer MockCommand()
    {
        var agg = new CustomerModelMock();
        agg.Name = Faker.Lorem.Sentence();
        agg.CPF = Faker.Lorem.Sentence();
        agg.phone = Faker.Lorem.Sentence();
        agg.email = Faker.Lorem.Sentence();
        return agg;
    }

    public CustomerService MockCustomerService()
    {
        OutPutDomainEvents = new List<DomainEvent>();
        var state = new CustomerStateMock();
        var dp = new DpMock<ICustomerState>(state, (domainevent) =>
        {
            OutPutDomainEvents.Add(domainevent);
        });
        var srv = new CustomerService(state, dp);
        return srv;
    }
}