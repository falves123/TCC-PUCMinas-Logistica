namespace Tests_Application;
public class DeliveryStateMock : IDeliveryState
{
    public IDeliveryRepository Delivery { get; set; }
    public DeliveryStateMock()
    {
    }

    public DeliveryStateMock(IDeliveryRepository delivery)
    {
        Delivery = delivery;
    }
}