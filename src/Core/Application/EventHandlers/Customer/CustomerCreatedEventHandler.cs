namespace Application.EventHandlers.Customer;
public class CustomerCreatedEventHandler : EventHandler<CustomerCreated, ICustomerState>
{
    public CustomerCreatedEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(CustomerCreated customerCreated)
    {
        var success = false;
        var customer = customerCreated.Get<Domain.Aggregates.Customer.Customer>();
        Dp.State.Customer.Add(customer);
        var destination = Dp.Settings.Default("stream.customerevents");
        var eventName = "CustomerCreated";
        var eventData = new CustomerCreatedEventDTO()
        {ID = customer.ID, Name = customer.Name, CPF = customer.CPF, Phone = customer.Phone, Email = customer.Email};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}