using System;                             
using System.Linq;
using System.Collections.Generic;                    
                                                    
namespace DevPrime.Web.Models.Notification       
{                                                   
    public class Notification                     
    {                                                     
      public string Name { get; set; }
      public string Email { get; set; }
      public string Number { get; set; }
      public IList<System.String> Params { get; set; }
       

    public static Application.Services.Notification.Model.Notification FromWebToApplicationNotification(DevPrime.Web.Models.Notification.Notification source)
    {
      if(source is null)
        return new Application.Services.Notification.Model.Notification();
      Application.Services.Notification.Model.Notification model = new Application.Services.Notification.Model.Notification();
      model.Name = source.Name;
      model.Email = source.Email;
      model.Number = source.Number;
      model.Params = source.Params;
      return model;
    }
    public static List<Application.Services.Notification.Model.Notification> FromWebToApplicationNotification(IList<DevPrime.Web.Models.Notification.Notification> sourceList)
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
          model.Params = source.Params;
          modelList.Add(model);
        }
      }
      return modelList;
    }
    public virtual Application.Services.Notification.Model.Notification ToApplication()
    {
      var model = FromWebToApplicationNotification(this);
      return model;
    }
 
    }                                               
}                                                   
