namespace DevPrime.Web.Models.Delivery;
public class Delivery
{
    public DateTime Started { get; set; }
    public DateTime Finished { get; set; }
    public Guid OrderID { get; set; }
    public double Total { get; set; }
    public static Application.Services.Delivery.Model.Delivery ToApplication(DevPrime.Web.Models.Delivery.Delivery delivery)
    {
        if (delivery is null)
            return new Application.Services.Delivery.Model.Delivery();
        Application.Services.Delivery.Model.Delivery _delivery = new Application.Services.Delivery.Model.Delivery();
        _delivery.Started = delivery.Started;
        _delivery.Finished = delivery.Finished;
        _delivery.OrderID = delivery.OrderID;
        _delivery.Total = delivery.Total;
        return _delivery;
    }

    public static List<Application.Services.Delivery.Model.Delivery> ToApplication(IList<DevPrime.Web.Models.Delivery.Delivery> deliveryList)
    {
        List<Application.Services.Delivery.Model.Delivery> _deliveryList = new List<Application.Services.Delivery.Model.Delivery>();
        if (deliveryList != null)
        {
            foreach (var delivery in deliveryList)
            {
                Application.Services.Delivery.Model.Delivery _delivery = new Application.Services.Delivery.Model.Delivery();
                _delivery.Started = delivery.Started;
                _delivery.Finished = delivery.Finished;
                _delivery.OrderID = delivery.OrderID;
                _delivery.Total = delivery.Total;
                _deliveryList.Add(_delivery);
            }
        }
        return _deliveryList;
    }

    public virtual Application.Services.Delivery.Model.Delivery ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}