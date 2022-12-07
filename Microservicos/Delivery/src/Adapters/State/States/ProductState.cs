namespace DevPrime.State.States;
public class ProductState : IProductState
{
    public IProductRepository Product { get; set; }
    public ProductState(IProductRepository product)
    {
        Product = product;
    }
}