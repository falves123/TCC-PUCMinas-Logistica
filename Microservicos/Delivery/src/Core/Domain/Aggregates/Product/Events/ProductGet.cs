namespace Domain.Aggregates.Product.Events;
public class ProductGet : DomainEvent
{
    public ProductGet() : base()
    {
    }

    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Ordering { get; set; }
    public string Filter { get; set; }
    public string Sort { get; set; }
}