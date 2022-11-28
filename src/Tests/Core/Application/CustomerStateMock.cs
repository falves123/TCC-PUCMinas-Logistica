namespace Tests_Application;
public class CustomerStateMock : ICustomerState
{
    public ICustomerRepository Customer { get; set; }
    public CustomerStateMock()
    {
    }

    public CustomerStateMock(ICustomerRepository customer)
    {
        Customer = customer;
    }
}