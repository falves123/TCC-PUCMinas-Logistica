using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevPrime.State.Repositories.Notification.Model  
{
    public class Notification
    {
      [BsonId]
      [BsonElement("_id")]
      public ObjectId Id { get; set; }
 
      [BsonRepresentation(BsonType.String)]
      public Guid NotificationID { get; set; }
      public string Name { get; set; }
      public string Email { get; set; }
      public string Number { get; set; }
      public IList<System.String> Params { get; set; }

    }
}
