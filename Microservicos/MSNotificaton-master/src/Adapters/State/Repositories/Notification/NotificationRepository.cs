using System;
using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using DevPrime.Stack.Foundation.State;
using Application.Interfaces.Adapters.State;
using DevPrime.State.Connections;

namespace DevPrime.State.Repositories.Notification
{
  public class NotificationRepository : RepositoryBase,INotificationRepository
  {
    public NotificationRepository(IDpState dp) : base(dp)
    {
      ConnectionAlias = "State1";
    }
    #region Write
    public void Add(Domain.Aggregates.Notification.Notification source)
    {
      Dp.Pipeline(Execute: (stateContext) =>
      {
        var state = new ConnectionMongo(stateContext);
        var model = FromDomainToStateNotification(source); 
        state.Notification.InsertOne(model);
      });
    }
    public void Delete(Guid id)
    {
      Dp.Pipeline(Execute: (stateContext) =>
      {
        var state = new ConnectionMongo(stateContext);
        state.Notification.DeleteOne(prop => prop.NotificationID == id);
      });
    }
    public void Update(Domain.Aggregates.Notification.Notification source)
    {
      Dp.Pipeline(Execute: (stateContext) =>
      {
        var state = new ConnectionMongo(stateContext);
        var model = FromDomainToStateNotification(source); 
        model.Id = state.Notification.Find(p => p.NotificationID == source.ID).FirstOrDefault().Id;
        state.Notification.ReplaceOne(p => p.NotificationID == source.ID, model);
      }); 
    }
    #endregion Write
    #region Read
    public Domain.Aggregates.Notification.Notification Get(Guid Id)
    {
      return Dp.Pipeline(ExecuteResult: (stateContext) =>
      {
        var state = new ConnectionMongo(stateContext);
        var source = state.Notification.Find(p => p.NotificationID == Id).FirstOrDefault();
        var model = FromStateToDomainNotification(source); 
        return model;
      });
    }
    public List<Domain.Aggregates.Notification.Notification> GetAll()
    {
      return Dp.Pipeline(ExecuteResult: (stateContext) =>
      {
        var state = new ConnectionMongo(stateContext);
        var source = state.Notification.Find(_ => true).ToList();
        var model = FromStateToDomainNotification(source); 
        return model;
       });
    }
    #endregion Read
    #region mappers

    public static DevPrime.State.Repositories.Notification.Model.Notification FromDomainToStateNotification(Domain.Aggregates.Notification.Notification source)
    {
      if(source is null)
        return new DevPrime.State.Repositories.Notification.Model.Notification();
      DevPrime.State.Repositories.Notification.Model.Notification model = new DevPrime.State.Repositories.Notification.Model.Notification();
      model.Name = source.Name;
      model.Email = source.Email;
      model.Number = source.Number;
      model.Params = source.Parameters;
      model.NotificationID = source.ID;
      return model;
    }
    public static Domain.Aggregates.Notification.Notification FromStateToDomainNotification(DevPrime.State.Repositories.Notification.Model.Notification source)
    {
      if(source is null)
        return new Domain.Aggregates.Notification.Notification(){ IsNew = true };
      Domain.Aggregates.Notification.Notification model = new Domain.Aggregates.Notification.Notification(
      source.Name,
      source.Email,
      source.Number,
      source.Params,
      source.NotificationID
      );
      return model;
    }
    public static List<Domain.Aggregates.Notification.Notification> FromStateToDomainNotification(IList<DevPrime.State.Repositories.Notification.Model.Notification> sourceList)
    {
      List<Domain.Aggregates.Notification.Notification> modelList = new List<Domain.Aggregates.Notification.Notification>();
      if(sourceList != null)
      {
        foreach(var source in sourceList)
        {
          Domain.Aggregates.Notification.Notification model = new Domain.Aggregates.Notification.Notification(
          source.Name,
          source.Email,
          source.Number,
          source.Params,
          source.NotificationID
          );
          modelList.Add(model);
        }
      }
      return modelList;
    }
 
    #endregion mappers
  }
}