namespace Application.EventHandlers.Delivery;
public class DeliveryDeletedEventHandler : EventHandler<DeliveryDeleted, IDeliveryState>
{
    public DeliveryDeletedEventHandler(IDeliveryState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(DeliveryDeleted deliveryDeleted)
    {
        var success = false;
        var delivery = deliveryDeleted.Get<Domain.Aggregates.Delivery.Delivery>();
        Dp.State.Delivery.Delete(delivery.ID);
        var destination = Dp.Settings.Default("stream.deliveryevents");
        var eventName = "DeliveryDeleted";
        var eventData = new DeliveryDeletedEventDTO()
        {ID = delivery.ID, Started = delivery.Started, Finished = delivery.Finished, OrderID = delivery.OrderID, Total = delivery.Total};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}