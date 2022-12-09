using System;                             
using System.Linq;
using System.Collections.Generic;                    
                                                    
namespace Application.Services.Notification.Model      
{                                                   
  public class NotificationCreatedEventDTO                     
  {                                                     
      public string Name { get; set;} 
      public string Email { get; set;} 
      public string Number { get; set;} 
      public Guid ID { get; set;} 
       
  }                                               
}                                                 