namespace DevPrime.State.Repositories.Payment.Model;
public class Payment
{
    public Guid PaymentID { get; set; }
    public string CustomerName { get; set; }
    public Guid OrderID { get; set; }
    public double Value { get; set; }
}