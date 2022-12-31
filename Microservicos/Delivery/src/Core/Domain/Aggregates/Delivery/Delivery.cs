namespace Domain.Aggregates.Delivery;
public class Delivery : AggRoot
{
    public DateTime Started { get; private set; }
    public DateTime Finished { get; private set; }
    public Guid OrderID { get; private set; }
    public double Total { get; private set; }
    public Delivery(Guid id, DateTime started, DateTime finished, Guid orderID, double total)
    {
        ID = id;
        Started = started;
        Finished = finished;
        OrderID = orderID;
        Total = total;
    }

    public Delivery()
    {
    }

    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ID = Guid.NewGuid();
            var ordertotal = Dp.ProcessEvent(new DeliveryGetOrder());
            if (ordertotal != null)
            {
                Total = ordertotal;
                Dp.ProcessEvent(new DeliveryCreated());
            }
            else
            {
                throw new PublicException("Can not confirm order");
            }
        });
    }

    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            Dp.ProcessEvent(new DeliveryUpdated());
        });
    }

    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
                Dp.ProcessEvent(new DeliveryDeleted());
        });
    }

    public virtual (List<Delivery> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
                    if (filter.ToLower().StartsWith("started="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("finished="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("orderid="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("total="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'Started', 'Finished', 'OrderID', 'Total',");
            }
            var source = Dp.ProcessEvent(new DeliveryGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    private void ValidFields()
    {
        if (Started == DateTime.MinValue)
            Dp.Notifications.Add("Started is required");
        if (Finished == DateTime.MinValue)
            Dp.Notifications.Add("Finished is required");
        Dp.Notifications.ValidateAndThrow();
    }
}