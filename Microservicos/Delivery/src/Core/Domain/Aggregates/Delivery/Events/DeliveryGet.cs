namespace Domain.Aggregates.Delivery.Events;
public class DeliveryGet : DomainEvent
{
    public DeliveryGet() : base()
    {
    }

    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Ordering { get; set; }
    public string Filter { get; set; }
    public string Sort { get; set; }
}