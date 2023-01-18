namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<OrderGet, OrderGetEventHandler>();
        handler.Add<PaymentCreated, PaymentCreatedEventHandler>();
        handler.Add<PaymentDeleted, PaymentDeletedEventHandler>();
        handler.Add<PaymentGet, PaymentGetEventHandler>();
        handler.Add<PaymentUpdated, PaymentUpdatedEventHandler>();
    }
}