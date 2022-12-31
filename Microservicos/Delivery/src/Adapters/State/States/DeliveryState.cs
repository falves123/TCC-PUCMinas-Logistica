namespace DevPrime.State.States;
public class DeliveryState : IDeliveryState
{
    public IDeliveryRepository Delivery { get; set; }
    public DeliveryState(IDeliveryRepository delivery)
    {
        Delivery = delivery;
    }
}