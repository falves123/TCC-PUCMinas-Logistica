namespace DevPrime.State.Repositories.Customer.Model;
public class Customer
{
    public Guid CustomerID { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}