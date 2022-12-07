namespace Application.EventHandlers.Product;
public class ProductGetEventHandler : EventHandler<ProductGet, IProductState>
{
    public ProductGetEventHandler(IProductState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(ProductGet domainEvent)
    {
        var source = Dp.State.Product.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.Product.Total(domainEvent.Filter);
        return (source, total);
    }
}