using Application.EventHandlers.Delivery.Model;

namespace Application.EventHandlers.Delivery;
public class DeliveryGetOrderEventHandler : EventHandler<DeliveryGetOrder, IDeliveryState>
{
    public DeliveryGetOrderEventHandler(IDeliveryState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(DeliveryGetOrder domainEvent)
    {
        var delivery = domainEvent.Get<Domain.Aggregates.Delivery.Delivery>();

        // External http request using Services Adapter
        var url = $"https://localhost:5001/v1/order/{delivery.OrderID}";

        // Yype dynamic
        var result = Dp.Services.HTTP.DpGet<OrderCreated>(url);

        //Analysis result
        if (result.Dp.Status.Equals(200))
            return result.Total;
           
        return null;
    }
}