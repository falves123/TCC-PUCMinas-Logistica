namespace Application.Services.Payment.Model;
public class OrderCreatedEventDTO
{
    public Guid ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerTaxID { get; set; }
    public double Total { get; set; }
}