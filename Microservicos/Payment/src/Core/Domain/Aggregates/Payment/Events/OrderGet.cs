namespace Domain.Aggregates.Payment.Events;

public class OrderGet : DomainEvent
{
    public Guid OrderId { get; set; }

    public OrderGet(Guid orderId) : base()
    {
        OrderId = orderId;
    }
}