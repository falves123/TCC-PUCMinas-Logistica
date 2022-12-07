namespace Application.Services.Order.Model;
public class OrderDeletedEventDTO
{
    public Guid ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerTaxID { get; set; }
    public string Zipcode { get; set; }
    public string Address { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string UF { get; set; }
    public string Complement { get; set; }    
    public double Total { get; set; }
}