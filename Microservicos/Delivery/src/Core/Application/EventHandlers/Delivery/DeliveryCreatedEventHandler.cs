namespace Application.EventHandlers.Delivery;
public class DeliveryCreatedEventHandler : EventHandler<DeliveryCreated, IDeliveryState>
{
    public DeliveryCreatedEventHandler(IDeliveryState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(DeliveryCreated deliveryCreated)
    {
        var success = false;
        var delivery = deliveryCreated.Get<Domain.Aggregates.Delivery.Delivery>();
        Dp.State.Delivery.Add(delivery);
        success = true;

        return success;
    }
}