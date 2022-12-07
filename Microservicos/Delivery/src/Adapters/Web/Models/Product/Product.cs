namespace DevPrime.Web.Models.Product;
public class Product
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Sku { get; set; }
    public int Quantity { get; set; }
    public static Application.Services.Product.Model.Product ToApplication(DevPrime.Web.Models.Product.Product product)
    {
        if (product is null)
            return new Application.Services.Product.Model.Product();
        Application.Services.Product.Model.Product _product = new Application.Services.Product.Model.Product();
        _product.Name = product.Name;
        _product.Description = product.Description;
        _product.Sku = product.Sku;
        _product.Quantity = product.Quantity;
        return _product;
    }

    public static List<Application.Services.Product.Model.Product> ToApplication(IList<DevPrime.Web.Models.Product.Product> productList)
    {
        List<Application.Services.Product.Model.Product> _productList = new List<Application.Services.Product.Model.Product>();
        if (productList != null)
        {
            foreach (var product in productList)
            {
                Application.Services.Product.Model.Product _product = new Application.Services.Product.Model.Product();
                _product.Name = product.Name;
                _product.Description = product.Description;
                _product.Sku = product.Sku;
                _product.Quantity = product.Quantity;
                _productList.Add(_product);
            }
        }
        return _productList;
    }

    public virtual Application.Services.Product.Model.Product ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}