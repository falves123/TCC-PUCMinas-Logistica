namespace Application.Services.Customer.Model;
public class CustomerCreatedEventDTO
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}