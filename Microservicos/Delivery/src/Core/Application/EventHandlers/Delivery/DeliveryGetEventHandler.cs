namespace Application.EventHandlers.Delivery;
public class DeliveryGetEventHandler : EventHandler<DeliveryGet, IDeliveryState>
{
    public DeliveryGetEventHandler(IDeliveryState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(DeliveryGet domainEvent)
    {
        var source = Dp.State.Delivery.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.Delivery.Total(domainEvent.Filter);
        return (source, total);
    }
}