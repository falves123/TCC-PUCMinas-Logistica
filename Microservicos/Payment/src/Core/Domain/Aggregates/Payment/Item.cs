namespace Domain.Aggregates.Payment;
public class Item : ValueObject
{
    public Guid ProductID { get; set; }

    public string Description { get; set; }

    public Item() { }

    public Item(Guid productID, string description)
    {
        ProductID = productID;
        Description = description;
    }
}