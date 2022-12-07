namespace Tests_Application;
public class ProductStateMock : IProductState
{
    public IProductRepository Product { get; set; }
    public ProductStateMock()
    {
    }

    public ProductStateMock(IProductRepository product)
    {
        Product = product;
    }
}