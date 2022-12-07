namespace Tests_Domain.Product;
public partial class ProductAggRootTest
{
    public Domain.Aggregates.Product.Product MockProduct()
    {
        var agg = new Domain.Aggregates.Product.Product(Guid.Empty, String.Empty, String.Empty, String.Empty, 0);
        agg.Dp = new DpDomainMock(null);
        agg.DevPrimeSetProperty<Guid>("ID", Guid.NewGuid());
        agg.DevPrimeSetProperty<String>("Name", Faker.Lorem.Sentence());
        agg.DevPrimeSetProperty<String>("Description", Faker.Lorem.Sentence());
        agg.DevPrimeSetProperty<String>("Sku", Faker.Lorem.Sentence());
        agg.DevPrimeSetProperty<Int32>("Quantity", Faker.RandomNumber.Next());
        agg.Dp.Source = agg;
        return agg;
    }
}