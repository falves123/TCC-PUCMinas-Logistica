using System;                             
using System.Linq;
using System.Collections.Generic;                    
                                                    
namespace Application.Services.Notification.Model       
{                                                   
  public class Notification                     
  {                                                     
    public string Name { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public IList<System.String> Params { get; set; }
    public Guid ID { get; set; }
    public virtual IList<Notification> ToNotificationList(IList<Domain.Aggregates.Notification.Notification> sourceList)
    {
      var modelList = FromDomainToApplicationNotification(sourceList);
      return modelList;
    }
    public virtual Notification ToNotification(Domain.Aggregates.Notification.Notification source)
    {
      var model = FromDomainToApplicationNotification(source);
      return model;
    }
    public virtual Domain.Aggregates.Notification.Notification ToNotificationDomain()
    {
      var model = FromApplicationToDomainNotification(this);
      return model;
    }
    public virtual Domain.Aggregates.Notification.Notification ToNotificationDomain(Guid id)
    {
      var model = new Domain.Aggregates.Notification.Notification();
      model.ID = id;
      return model;
    }
    public Notification()
    {
    }
    public Notification(Guid id)
    {
      ID = id;
    }
    public static Application.Services.Notification.Model.Notification FromDomainToApplicationNotification(Domain.Aggregates.Notification.Notification source)
    {
      if(source is null)
        return new Application.Services.Notification.Model.Notification();
      Application.Services.Notification.Model.Notification model = new Application.Services.Notification.Model.Notification();
      model.Name = source.Name;
      model.Email = source.Email;
      model.Number = source.Number;
      model.Params = source.Parameters;
      model.ID = source.ID;
      return model;
    }
    public static List<Application.Services.Notification.Model.Notification> FromDomainToApplicationNotification(IList<Domain.Aggregates.Notification.Notification> sourceList)
    {
      List<Application.Services.Notification.Model.Notification> modelList = new List<Application.Services.Notification.Model.Notification>();
      if(sourceList != null)
      {
        foreach(var source in sourceList)
        {
          Application.Services.Notification.Model.Notification model = new Application.Services.Notification.Model.Notification();
          model.Name = source.Name;
          model.Email = source.Email;
          model.Number = source.Number;
          model.Params = source.Parameters;
          model.ID = source.ID;
          modelList.Add(model);
        }
      }
      return modelList;
    }
    public static Domain.Aggregates.Notification.Notification FromApplicationToDomainNotification(Application.Services.Notification.Model.Notification source)
    {
      if(source is null)
        return new Domain.Aggregates.Notification.Notification();
      Domain.Aggregates.Notification.Notification model = new Domain.Aggregates.Notification.Notification(
      source.Name,
      source.Email,
      source.Number,
      source.Params,
      source.ID
      );
      return model;
    }
    public static List<Domain.Aggregates.Notification.Notification> FromApplicationToDomainNotification(IList<Application.Services.Notification.Model.Notification> sourceList)
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
          source.ID
          );
          modelList.Add(model);
        }
      }
      return modelList;
    }
       
  }                                               
}                                                 