namespace Application.Services.Delivery.Model;
public class Delivery
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public Delivery(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }

    public Guid ID { get; set; }
    public DateTime Started { get; set; }
    public DateTime Finished { get; set; }
    public Guid OrderID { get; set; }
    public double Total { get; set; }
    public virtual PagingResult<IList<Delivery>> ToDeliveryList(IList<Domain.Aggregates.Delivery.Delivery> deliveryList, long? total, int? offSet, int? limit)
    {
        var _deliveryList = ToApplication(deliveryList);
        return new PagingResult<IList<Delivery>>(_deliveryList, total, offSet, limit);
    }

    public virtual Delivery ToDelivery(Domain.Aggregates.Delivery.Delivery delivery)
    {
        var _delivery = ToApplication(delivery);
        return _delivery;
    }

    public virtual Domain.Aggregates.Delivery.Delivery ToDomain()
    {
        var _delivery = ToDomain(this);
        return _delivery;
    }

    public virtual Domain.Aggregates.Delivery.Delivery ToDomain(Guid id)
    {
        var _delivery = new Domain.Aggregates.Delivery.Delivery();
        _delivery.ID = id;
        return _delivery;
    }

    public Delivery()
    {
    }

    public Delivery(Guid id)
    {
        ID = id;
    }

    public static Application.Services.Delivery.Model.Delivery ToApplication(Domain.Aggregates.Delivery.Delivery delivery)
    {
        if (delivery is null)
            return new Application.Services.Delivery.Model.Delivery();
        Application.Services.Delivery.Model.Delivery _delivery = new Application.Services.Delivery.Model.Delivery();
        _delivery.ID = delivery.ID;
        _delivery.Started = delivery.Started;
        _delivery.Finished = delivery.Finished;
        _delivery.OrderID = delivery.OrderID;
        _delivery.Total = delivery.Total;
        return _delivery;
    }

    public static List<Application.Services.Delivery.Model.Delivery> ToApplication(IList<Domain.Aggregates.Delivery.Delivery> deliveryList)
    {
        List<Application.Services.Delivery.Model.Delivery> _deliveryList = new List<Application.Services.Delivery.Model.Delivery>();
        if (deliveryList != null)
        {
            foreach (var delivery in deliveryList)
            {
                Application.Services.Delivery.Model.Delivery _delivery = new Application.Services.Delivery.Model.Delivery();
                _delivery.ID = delivery.ID;
                _delivery.Started = delivery.Started;
                _delivery.Finished = delivery.Finished;
                _delivery.OrderID = delivery.OrderID;
                _delivery.Total = delivery.Total;
                _deliveryList.Add(_delivery);
            }
        }
        return _deliveryList;
    }

    public static Domain.Aggregates.Delivery.Delivery ToDomain(Application.Services.Delivery.Model.Delivery delivery)
    {
        if (delivery is null)
            return new Domain.Aggregates.Delivery.Delivery();
        Domain.Aggregates.Delivery.Delivery _delivery = new Domain.Aggregates.Delivery.Delivery(delivery.ID, delivery.Started, delivery.Finished, delivery.OrderID, delivery.Total);
        return _delivery;
    }

    public static List<Domain.Aggregates.Delivery.Delivery> ToDomain(IList<Application.Services.Delivery.Model.Delivery> deliveryList)
    {
        List<Domain.Aggregates.Delivery.Delivery> _deliveryList = new List<Domain.Aggregates.Delivery.Delivery>();
        if (deliveryList != null)
        {
            foreach (var delivery in deliveryList)
            {
                Domain.Aggregates.Delivery.Delivery _delivery = new Domain.Aggregates.Delivery.Delivery(delivery.ID, delivery.Started, delivery.Finished, delivery.OrderID, delivery.Total);
                _deliveryList.Add(_delivery);
            }
        }
        return _deliveryList;
    }
}