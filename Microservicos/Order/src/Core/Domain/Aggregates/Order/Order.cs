namespace Domain.Aggregates.Order;
public class Order : AggRoot
{
    public string CustomerName { get; private set; }
    public string CustomerTaxID { get; private set; }
    public string Zipcode { get; private set; }
    public string Address { get; private set; }
    public string Number { get; private set; }
    public string City { get; private set; }
    public string UF { get; private set; }
    public string Complement { get; private set; }   
    public IList<Item> Items { get; private set; }
    public double Total { get; private set; }
    public void AddItem(Item item)
    {
        if (item != null && Items != null)
        {
            var myItems = Items.Where(p => p.SKU == item.SKU).FirstOrDefault();
            if (myItems != null)
                myItems.Sum(item.Amount);
            else
                Items.Add(item);
        }
    }

    public Order(Guid id, string customerName, string customerTaxID, string zipcode, string address, string number, string city, string uf, string complement, IEnumerable<Domain.Aggregates.Order.Item> items, double total)
    {
        ID = id;
        CustomerName = customerName;
        CustomerTaxID = customerTaxID;
        Zipcode = zipcode;
        Address = address;
        Number = number;
        City = city;
        UF = uf;
        Complement = complement;
        Items = items?.ToList();
        Total = total;
    }

    public Order()
    {
    }

    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            Dp.Attach(Items);
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            Dp.ProcessEvent(new OrderCreated());
        });
    }

    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            Dp.Attach(Items);
            ValidFields();
            Dp.ProcessEvent(new OrderUpdated());
        });
    }

    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
                Dp.ProcessEvent(new OrderDeleted());
        });
    }

    public virtual (List<Order> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
                    if (filter.ToLower().StartsWith("customername="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("customertaxid="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("zipcode="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("address="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("number="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("city="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("uf="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("complement="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("total="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'CustomerName', 'CustomerTaxID', 'Zipcode', 'Address', 'Number', 'City', 'UF', 'Complement', 'Total',");
            }
            var source = Dp.ProcessEvent(new OrderGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(CustomerName))
            Dp.Notifications.Add("CustomerName is required");
        if (String.IsNullOrWhiteSpace(CustomerTaxID))
            Dp.Notifications.Add("CustomerTaxID is required");
        if (String.IsNullOrWhiteSpace(Zipcode))
            Dp.Notifications.Add("Zipcode is required");
        if (String.IsNullOrWhiteSpace(Address))
            Dp.Notifications.Add("Address is required");
        if (String.IsNullOrWhiteSpace(Number))
            Dp.Notifications.Add("Number is required");
        if (String.IsNullOrWhiteSpace(City))
            Dp.Notifications.Add("City is required");
        if (String.IsNullOrWhiteSpace(UF))
            Dp.Notifications.Add("UF is required");
        Dp.Notifications.ValidateAndThrow();
    }
}