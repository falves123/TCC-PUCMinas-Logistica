namespace Application.Services.Product.Model;
public class Product
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public Product(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }

    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Sku { get; set; }
    public int Quantity { get; set; }
    public virtual PagingResult<IList<Product>> ToProductList(IList<Domain.Aggregates.Product.Product> productList, long? total, int? offSet, int? limit)
    {
        var _productList = ToApplication(productList);
        return new PagingResult<IList<Product>>(_productList, total, offSet, limit);
    }

    public virtual Product ToProduct(Domain.Aggregates.Product.Product product)
    {
        var _product = ToApplication(product);
        return _product;
    }

    public virtual Domain.Aggregates.Product.Product ToDomain()
    {
        var _product = ToDomain(this);
        return _product;
    }

    public virtual Domain.Aggregates.Product.Product ToDomain(Guid id)
    {
        var _product = new Domain.Aggregates.Product.Product();
        _product.ID = id;
        return _product;
    }

    public Product()
    {
    }

    public Product(Guid id)
    {
        ID = id;
    }

    public static Application.Services.Product.Model.Product ToApplication(Domain.Aggregates.Product.Product product)
    {
        if (product is null)
            return new Application.Services.Product.Model.Product();
        Application.Services.Product.Model.Product _product = new Application.Services.Product.Model.Product();
        _product.ID = product.ID;
        _product.Name = product.Name;
        _product.Description = product.Description;
        _product.Sku = product.Sku;
        _product.Quantity = product.Quantity;
        return _product;
    }

    public static List<Application.Services.Product.Model.Product> ToApplication(IList<Domain.Aggregates.Product.Product> productList)
    {
        List<Application.Services.Product.Model.Product> _productList = new List<Application.Services.Product.Model.Product>();
        if (productList != null)
        {
            foreach (var product in productList)
            {
                Application.Services.Product.Model.Product _product = new Application.Services.Product.Model.Product();
                _product.ID = product.ID;
                _product.Name = product.Name;
                _product.Description = product.Description;
                _product.Sku = product.Sku;
                _product.Quantity = product.Quantity;
                _productList.Add(_product);
            }
        }
        return _productList;
    }

    public static Domain.Aggregates.Product.Product ToDomain(Application.Services.Product.Model.Product product)
    {
        if (product is null)
            return new Domain.Aggregates.Product.Product();
        Domain.Aggregates.Product.Product _product = new Domain.Aggregates.Product.Product(product.ID, product.Name, product.Description, product.Sku, product.Quantity);
        return _product;
    }

    public static List<Domain.Aggregates.Product.Product> ToDomain(IList<Application.Services.Product.Model.Product> productList)
    {
        List<Domain.Aggregates.Product.Product> _productList = new List<Domain.Aggregates.Product.Product>();
        if (productList != null)
        {
            foreach (var product in productList)
            {
                Domain.Aggregates.Product.Product _product = new Domain.Aggregates.Product.Product(product.ID, product.Name, product.Description, product.Sku, product.Quantity);
                _productList.Add(_product);
            }
        }
        return _productList;
    }
}