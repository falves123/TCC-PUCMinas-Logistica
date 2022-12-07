namespace DevPrime.State.Repositories.Order.Model;
public class Item
{
    public Guid OrderID { get; set; }
    public Guid ItemID { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public string SKU { get; set; }
    public double Price { get; set; }
}