namespace Application.Services.Product.Model;
public class ProductDeletedEventDTO
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Sku { get; set; }
    public int Quantity { get; set; }
}