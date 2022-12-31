namespace Application.EventHandlers.Delivery;
public class DeliveryUpdatedEventHandler : EventHandler<DeliveryUpdated, IDeliveryState>
{
    public DeliveryUpdatedEventHandler(IDeliveryState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(DeliveryUpdated deliveryUpdated)
    {
        var success = false;
        var delivery = deliveryUpdated.Get<Domain.Aggregates.Delivery.Delivery>();
        Dp.State.Delivery.Update(delivery);
        var destination = Dp.Settings.Default("stream.deliveryevents");
        var eventName = "DeliveryUpdated";
        var eventData = new DeliveryUpdatedEventDTO()
        {ID = delivery.ID, Started = delivery.Started, Finished = delivery.Finished, OrderID = delivery.OrderID, Total = delivery.Total};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}