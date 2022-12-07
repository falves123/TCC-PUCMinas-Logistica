namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<ProductCreated, ProductCreatedEventHandler>();
        handler.Add<ProductDeleted, ProductDeletedEventHandler>();
        handler.Add<ProductGet, ProductGetEventHandler>();
        handler.Add<ProductUpdated, ProductUpdatedEventHandler>();
    }
}