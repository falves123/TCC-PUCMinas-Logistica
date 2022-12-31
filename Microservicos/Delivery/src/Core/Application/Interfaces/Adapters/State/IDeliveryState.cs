namespace Application.Interfaces.Adapters.State;
public interface IDeliveryState
{
    IDeliveryRepository Delivery { get; set; }
}