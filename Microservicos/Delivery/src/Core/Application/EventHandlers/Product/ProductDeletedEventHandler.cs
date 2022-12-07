namespace Application.EventHandlers.Product;
public class ProductDeletedEventHandler : EventHandler<ProductDeleted, IProductState>
{
    public ProductDeletedEventHandler(IProductState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(ProductDeleted productDeleted)
    {
        var success = false;
        var product = productDeleted.Get<Domain.Aggregates.Product.Product>();
        Dp.State.Product.Delete(product.ID);
        var destination = Dp.Settings.Default("stream.productevents");
        var eventName = "ProductDeleted";
        var eventData = new ProductDeletedEventDTO()
        {ID = product.ID, Name = product.Name, Description = product.Description, Sku = product.Sku, Quantity = product.Quantity};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}