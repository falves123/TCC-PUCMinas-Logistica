namespace Domain.Aggregates.Product;
public class Product : AggRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Sku { get; private set; }
    public int Quantity { get; private set; }
    public Product(Guid id, string name, string description, string sku, int quantity)
    {
        ID = id;
        Name = name;
        Description = description;
        Sku = sku;
        Quantity = quantity;
    }

    public Product()
    {
    }

    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            Dp.ProcessEvent(new ProductCreated());
        });
    }

    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            Dp.ProcessEvent(new ProductUpdated());
        });
    }

    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
                Dp.ProcessEvent(new ProductDeleted());
        });
    }

    public virtual (List<Product> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
                    if (filter.ToLower().StartsWith("description="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("sku="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("quantity="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'Name', 'Description', 'Sku', 'Quantity',");
            }
            var source = Dp.ProcessEvent(new ProductGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(Name))
            Dp.Notifications.Add("Name is required");
        if (String.IsNullOrWhiteSpace(Description))
            Dp.Notifications.Add("Description is required");
        if (String.IsNullOrWhiteSpace(Sku))
            Dp.Notifications.Add("Sku is required");
        Dp.Notifications.ValidateAndThrow();
    }
}