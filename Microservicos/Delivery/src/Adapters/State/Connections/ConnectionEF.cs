namespace DevPrime.State.Connections;
public class ConnectionEF : EFBaseState
{
    public DbSet<DevPrime.State.Repositories.Product.Model.Product> Product { get; set; }
    public ConnectionEF(DbContextOptions<ConnectionEF> options) : base(options)
    {
    }

    public ConnectionEF()
    {
    }
}