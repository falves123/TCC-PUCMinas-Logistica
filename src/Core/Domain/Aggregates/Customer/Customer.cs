namespace Domain.Aggregates.Customer;
public class Customer : AggRoot
{
    public string Name { get; private set; }
    public string CPF { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public Customer(Guid id, string name, string cpf, string phone, string email)
    {
        ID = id;
        Name = name;
        CPF = cpf;
        Phone = phone;
        Email = email;
    }

    public Customer()
    {
    }

    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            Dp.ProcessEvent(new CustomerCreated());
        });
    }

    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            Dp.ProcessEvent(new CustomerUpdated());
        });
    }

    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
                Dp.ProcessEvent(new CustomerDeleted());
        });
    }

    public virtual (List<Customer> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            ValidateOrdering(limit, offset, ordering, sort);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                bool filterIsValid = false;
                if (filter.Contains("="))
                {
                    if (filter.ToLower().StartsWith("id="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("name="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("cpf="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("phone="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("email="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'Name', 'Cpf', 'Phone', 'Email',");
            }
            var source = Dp.ProcessEvent(new CustomerGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(Name))
            Dp.Notifications.Add("Name is required");
        if (String.IsNullOrWhiteSpace(CPF))
            Dp.Notifications.Add("Cpf is required");
        if (String.IsNullOrWhiteSpace(Phone))
            Dp.Notifications.Add("Phone is required");
        Dp.Notifications.ValidateAndThrow();
    }
}