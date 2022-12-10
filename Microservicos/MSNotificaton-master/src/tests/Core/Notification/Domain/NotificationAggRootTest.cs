using Domain.Aggregates.Notification.Events;
using DevPrime.Stack.Test;
using System;
using Xunit;
using DevPrime.Stack.Foundation.Exceptions;

namespace Tests_Domain.Notification
{
    public partial class NotificationAggRootTest
    {
        #region ValidateFields
        [Fact]
        [Trait("Category", "Validate")]
        [Trait("Category", "Success")]
        public void ValidateFields_FieldsAreValid_Success()
        {
            //Arrange
            var agg = MockNotification();
            //Act
            agg.DevPrimeCallMethod("ValidFields");
            //Assert
            Assert.True(agg.Dp.Notifications.IsValid);
        }
        #endregion ValidateFields
        #region Add
        [Fact]
        [Trait("Category", "Add")]
        [Trait("Category", "Success")]
        public void Add_AllFieldsFilled_TriggerEventNotificationCreated()
        {
            //Arrange
            var agg = MockNotification();
            //Act
            agg.Add();
            //Assert
            Assert.True(agg.Dp.GetDomainEvent() is NotificationCreated);
        }

        #endregion Add
        #region Update
        [Fact]
        [Trait("Category", "Update")]
        [Trait("Category", "Success")]
        public void Update_FieldsFilled_TriggerEventNotificationUpdated()
        {
            //Arrange
            var agg = MockNotification();
            //Act
            agg.Update();
            //Assert
            Assert.True(agg.Dp.GetDomainEvent() is NotificationUpdated);
        }
        #endregion Update
        #region Delete
        [Fact]
        [Trait("Category", "Delete")]
        [Trait("Category", "Success")]
        public void Delete_FieldsFilled_TriggerEventNotificationDeleted()
        {
            //Arrange
            var agg = MockNotification();
            agg.ID = Guid.NewGuid();
            //Act
            agg.Delete();
            //Assert
            Assert.True(agg.Dp.GetDomainEvent() is NotificationDeleted);
        }
        [Fact]
        [Trait("Category", "Delete")]
        [Trait("Category", "Failure")]
        public void Delete_FieldsNotFilled_DontTriggerEvent()
        {
            //Arrange
            var agg = MockNotification();
            agg.ID = Guid.Empty;
            //Act
            agg.Delete();
            //Assert
            Assert.False(agg.Dp.GetDomainEvent() is NotificationDeleted);
        }
        #endregion Delete
    }
}