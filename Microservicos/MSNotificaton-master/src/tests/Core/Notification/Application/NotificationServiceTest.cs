using DevPrime.Stack.Test;
using Domain.Aggregates.Notification.Events;
using System;
using Xunit;

namespace Tests_Application.Notification
{
    public class NotificationServiceTest
    {
        [Fact]
        [Trait("Category", "Add")]
        [Trait("Category", "Success")]
        public void Add_Command_Result()
        {
            //Arrange
            var serviceMock = new NotificationServiceMock();
            var command = serviceMock.MockCommand();
            var service = serviceMock.MockNotificationService();
            //Act
            service.Add(command);
            //Assert
            Assert.NotNull(serviceMock.OutPutDomainEvents[0] as NotificationCreated);
        }
        [Fact]
        [Trait("Category", "Update")]
        [Trait("Category", "Success")]
        public void Update_Command_Result()
        {
            //Arrange
            var serviceMock = new NotificationServiceMock();
            var command = serviceMock.MockCommand();
            var service = serviceMock.MockNotificationService();
            //Act
            service.Update(command);
            //Assert
            Assert.NotNull(serviceMock.OutPutDomainEvents[0] as NotificationUpdated);
        }
        [Fact]
        [Trait("Category", "Delete")]
        [Trait("Category", "Success")]
        public void Delete_Command_Result()
        {
            //Arrange
            var serviceMock = new NotificationServiceMock();
            var command = serviceMock.MockCommand();
            var service = serviceMock.MockNotificationService();
            //Act
            service.Delete(command);
            //Assert
            Assert.NotNull(serviceMock.OutPutDomainEvents[0] as NotificationDeleted);
        }
    }
 }
