namespace Application.EventHandlers.Product;
public class ProductCreatedEventHandler : EventHandler<ProductCreated, IProductState>
{
    public ProductCreatedEventHandler(IProductState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(ProductCreated productCreated)
    {
        var success = false;
        var product = productCreated.Get<Domain.Aggregates.Product.Product>();
        Dp.State.Product.Add(product);
        var destination = Dp.Settings.Default("stream.productevents");
        var eventName = "ProductCreated";
        var eventData = new ProductCreatedEventDTO()
        {ID = product.ID, Name = product.Name, Description = product.Description, Sku = product.Sku, Quantity = product.Quantity};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}