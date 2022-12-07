namespace Application.Interfaces.Services;
public interface IProductService
{
    void Add(Application.Services.Product.Model.Product command);
    void Update(Application.Services.Product.Model.Product command);
    void Delete(Application.Services.Product.Model.Product command);
    Application.Services.Product.Model.Product Get(Application.Services.Product.Model.Product query);
    PagingResult<IList<Application.Services.Product.Model.Product>> GetAll(Application.Services.Product.Model.Product query);
}