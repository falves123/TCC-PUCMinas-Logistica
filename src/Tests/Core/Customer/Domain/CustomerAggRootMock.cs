namespace Tests_Domain.Customer;
public partial class CustomerAggRootTest
{
    public Domain.Aggregates.Customer.Customer MockCustomer()
    {
        var agg = new Domain.Aggregates.Customer.Customer(Guid.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
        agg.Dp = new DpDomainMock(null);
        agg.DevPrimeSetProperty<Guid>("ID", Guid.NewGuid());
        agg.DevPrimeSetProperty<String>("Name", Faker.Lorem.Sentence());
        agg.DevPrimeSetProperty<String>("Cpf", Faker.Lorem.Sentence());
        agg.DevPrimeSetProperty<String>("Phone", Faker.Lorem.Sentence());
        agg.DevPrimeSetProperty<String>("Email", Faker.Lorem.Sentence());
        agg.Dp.Source = agg;
        return agg;
    }
}