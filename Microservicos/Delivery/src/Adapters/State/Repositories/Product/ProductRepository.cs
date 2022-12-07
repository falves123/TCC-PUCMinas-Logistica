namespace DevPrime.State.Repositories.Product;
public class ProductRepository : RepositoryBase, IProductRepository
{
    public ProductRepository(IDpState dp, ConnectionEF state) : base(dp)
    {
        ConnectionAlias = "State1";
        State = state;
    }

#region Write

    public void Add(Domain.Aggregates.Product.Product product)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _product = ToState(product);
            state.Product.Add(_product);
            state.SaveChanges();
        });
    }

    public void Delete(Guid productID)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _product = state.Product.FirstOrDefault(b => b.ProductID == productID);
            state.Remove(_product);
            state.SaveChanges();
        });
    }

    public void Update(Domain.Aggregates.Product.Product product)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _product = ToState(product);
            state.Update(_product);
            state.SaveChanges();
        });
    }

#endregion Write

#region Read

    public Domain.Aggregates.Product.Product Get(Guid productID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var product = state.Product.FirstOrDefault(b => b.ProductID == productID);
            var _product = ToDomain(product);
            return _product;
        });
    }

    public List<Domain.Aggregates.Product.Product> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            List<Model.Product> product = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.Product.Where(GetFilter(filter)).OrderByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    product = result.Skip(((int)offset - 1) * (int)limit).Take((int)limit).ToList();
                else
                    product = result.ToList();
            }
            else
            {
                var result = state.Product.Where(GetFilter(filter)).OrderBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    product = result.Skip(((int)offset - 1) * (int)limit).Take((int)limit).ToList();
                else
                    product = result.ToList();
            }
            var _product = ToDomain(product);
            return _product;
        });
    }
    private Expression<Func<Model.Product, object>> GetOrdering(string field)
    {
        Expression<Func<Model.Product, object>> exp = p => p.ProductID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "name")
                exp = p => p.Name;
            else if (field.ToLower() == "description")
                exp = p => p.Description;
            else if (field.ToLower() == "sku")
                exp = p => p.Sku;
            else if (field.ToLower() == "quantity")
                exp = p => p.Quantity;
            else
                exp = p => p.ProductID;
        }
        return exp;
    }
    private Expression<Func<Model.Product, bool>> GetFilter(string filter)
    {
        Expression<Func<Model.Product, bool>> exp = p => true;
        if (!string.IsNullOrWhiteSpace(filter))
        {
            var slice = filter?.Split("=");
            if (slice.Length > 1)
            {
                var field = slice[0];
                var value = slice[1];
                if (string.IsNullOrWhiteSpace(value))
                {
                    exp = p => true;
                }
                else
                {
                    if (field.ToLower() == "name")
                        exp = p => p.Name.ToLower() == value.ToLower();
                    else if (field.ToLower() == "description")
                        exp = p => p.Description.ToLower() == value.ToLower();
                    else if (field.ToLower() == "sku")
                        exp = p => p.Sku.ToLower() == value.ToLower();
                    else if (field.ToLower() == "quantity")
                        exp = p => p.Quantity == Convert.ToInt32(value);
                    else
                        exp = p => true;
                }
            }
        }
        return exp;
    }

    public bool Exists(Guid productID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var product = state.Product.Where(x => x.ProductID == productID).FirstOrDefault();
            return (productID == product?.ProductID);
        });
    }

    public long Total(string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var total = state.Product.Where(GetFilter(filter)).Count();
            return total;
        });
    }

#endregion Read

#region mappers

    public static DevPrime.State.Repositories.Product.Model.Product ToState(Domain.Aggregates.Product.Product product)
    {
        if (product is null)
            return new DevPrime.State.Repositories.Product.Model.Product();
        DevPrime.State.Repositories.Product.Model.Product _product = new DevPrime.State.Repositories.Product.Model.Product();
        _product.ProductID = product.ID;
        _product.Name = product.Name;
        _product.Description = product.Description;
        _product.Sku = product.Sku;
        _product.Quantity = product.Quantity;
        return _product;
    }

    public static Domain.Aggregates.Product.Product ToDomain(DevPrime.State.Repositories.Product.Model.Product product)
    {
        if (product is null)
            return new Domain.Aggregates.Product.Product()
            {IsNew = true};
        Domain.Aggregates.Product.Product _product = new Domain.Aggregates.Product.Product(product.ProductID, product.Name, product.Description, product.Sku, product.Quantity);
        return _product;
    }

    public static List<Domain.Aggregates.Product.Product> ToDomain(IList<DevPrime.State.Repositories.Product.Model.Product> productList)
    {
        List<Domain.Aggregates.Product.Product> _productList = new List<Domain.Aggregates.Product.Product>();
        if (productList != null)
        {
            foreach (var product in productList)
            {
                Domain.Aggregates.Product.Product _product = new Domain.Aggregates.Product.Product(product.ProductID, product.Name, product.Description, product.Sku, product.Quantity);
                _productList.Add(_product);
            }
        }
        return _productList;
    }

#endregion mappers
}