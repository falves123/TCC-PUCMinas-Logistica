namespace DevPrime.State.Connections;
public class ConnectionEF : EFBaseState
{
    public DbSet<DevPrime.State.Repositories.Payment.Model.Payment> Payment { get; set; }
    public ConnectionEF(DbContextOptions<ConnectionEF> options) : base(options)
    {
    }

    public ConnectionEF()
    {
    }
}