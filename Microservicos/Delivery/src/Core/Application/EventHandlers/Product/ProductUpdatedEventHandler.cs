namespace Application.EventHandlers.Product;
public class ProductUpdatedEventHandler : EventHandler<ProductUpdated, IProductState>
{
    public ProductUpdatedEventHandler(IProductState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(ProductUpdated productUpdated)
    {
        var success = false;
        var product = productUpdated.Get<Domain.Aggregates.Product.Product>();
        Dp.State.Product.Update(product);
        var destination = Dp.Settings.Default("stream.productevents");
        var eventName = "ProductUpdated";
        var eventData = new ProductUpdatedEventDTO()
        {ID = product.ID, Name = product.Name, Description = product.Description, Sku = product.Sku, Quantity = product.Quantity};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}