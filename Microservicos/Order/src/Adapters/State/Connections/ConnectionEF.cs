namespace DevPrime.State.Connections;
public class ConnectionEF : EFBaseState
{
    public DbSet<DevPrime.State.Repositories.Order.Model.Order> Order { get; set; }
    public ConnectionEF(DbContextOptions<ConnectionEF> options) : base(options)
    {
    }

    public ConnectionEF()
    {
    }
}