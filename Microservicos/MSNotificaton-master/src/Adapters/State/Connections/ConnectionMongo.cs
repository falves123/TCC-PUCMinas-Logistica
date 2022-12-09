using MongoDB.Driver;
using DevPrime.Stack.State.MongoDB;
using DevPrime.State.Repositories.Notification.Model;


namespace DevPrime.State.Connections
{
  public class ConnectionMongo : MongoBaseState
  {
    public ConnectionMongo(MongoBaseState stateContext) : base(stateContext)
    {
    }
    public IMongoCollection<DevPrime.State.Repositories.Notification.Model.Notification> Notification
    {
        get
        {
          return DB.GetCollection<DevPrime.State.Repositories.Notification.Model.Notification>("Notification");
        }
    }

  }
}
