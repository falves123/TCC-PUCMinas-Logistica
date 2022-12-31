namespace Application.Interfaces.Services;
public interface IDeliveryService
{
    void Add(Application.Services.Delivery.Model.Delivery command);
    void Update(Application.Services.Delivery.Model.Delivery command);
    void Delete(Application.Services.Delivery.Model.Delivery command);
    Application.Services.Delivery.Model.Delivery Get(Application.Services.Delivery.Model.Delivery query);
    PagingResult<IList<Application.Services.Delivery.Model.Delivery>> GetAll(Application.Services.Delivery.Model.Delivery query);
}