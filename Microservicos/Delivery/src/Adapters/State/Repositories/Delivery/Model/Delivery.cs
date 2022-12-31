namespace DevPrime.State.Repositories.Delivery.Model;
public class Delivery
{
    public Guid DeliveryID { get; set; }
    public DateTime Started { get; set; }
    public DateTime Finished { get; set; }
    public Guid OrderID { get; set; }
    public double Total { get; set; }
}