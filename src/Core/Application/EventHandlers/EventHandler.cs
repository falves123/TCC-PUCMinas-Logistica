namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<CustomerCreated, CustomerCreatedEventHandler>();
        handler.Add<CustomerDeleted, CustomerDeletedEventHandler>();
        handler.Add<CustomerGet, CustomerGetEventHandler>();
        handler.Add<CustomerUpdated, CustomerUpdatedEventHandler>();
    }
}