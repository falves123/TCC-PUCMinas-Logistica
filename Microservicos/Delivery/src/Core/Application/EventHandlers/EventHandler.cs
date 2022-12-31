namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<DeliveryCreated, DeliveryCreatedEventHandler>();
        handler.Add<DeliveryDeleted, DeliveryDeletedEventHandler>();
        handler.Add<DeliveryGet, DeliveryGetEventHandler>();
        handler.Add<DeliveryGetOrder, DeliveryGetOrderEventHandler>();
        handler.Add<DeliveryUpdated, DeliveryUpdatedEventHandler>();
        handler.Add<ProductCreated, ProductCreatedEventHandler>();
        handler.Add<ProductDeleted, ProductDeletedEventHandler>();
        handler.Add<ProductGet, ProductGetEventHandler>();
        handler.Add<ProductUpdated, ProductUpdatedEventHandler>();
    }
}