using System;
using System.Collections.Generic;
using DevPrime.Stack.Test;

namespace Tests_Domain.Notification
{
    public partial class NotificationAggRootTest
    {
        public Domain.Aggregates.Notification.Notification MockNotification()
        {
            var agg = new Domain.Aggregates.Notification.Notification(
                       String.Empty,
                       String.Empty,
                       String.Empty,
                       null,
                       Guid.Empty

             );

            agg.Dp = new DpDomainMock(null);
            agg.DevPrimeSetProperty<String>("Name", Faker.Lorem.Sentence());
            agg.DevPrimeSetProperty<String>("Email", Faker.Lorem.Sentence());
            agg.DevPrimeSetProperty<String>("Number", Faker.Lorem.Sentence());
            agg.DevPrimeSetProperty<Guid>("ID", Guid.NewGuid());
        
            agg.Dp.Source = agg;
            return agg;
        }
    }
}