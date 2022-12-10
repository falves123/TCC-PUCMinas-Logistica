namespace DevPrime.State.Repositories.Order.Model;
public class Order
{
    public Guid OrderID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerTaxID { get; set; }
    public IList<DevPrime.State.Repositories.Order.Model.Item> Items { get; set; }
    public double Total { get; set; }
}