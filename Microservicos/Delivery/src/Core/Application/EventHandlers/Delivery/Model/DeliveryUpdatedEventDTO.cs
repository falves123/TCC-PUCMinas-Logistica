namespace Application.Services.Delivery.Model;
public class DeliveryUpdatedEventDTO
{
    public Guid ID { get; set; }
    public DateTime Started { get; set; }
    public DateTime Finished { get; set; }
    public Guid OrderID { get; set; }
    public double Total { get; set; }
}