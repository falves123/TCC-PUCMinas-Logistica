namespace Tests_Application.Delivery;
public class DeliveryServiceTest
{
    [Fact]
    [Trait("Category", "Add")]
    [Trait("Category", "Success")]

    public void Add_Command_Result()
    {
        //Arrange
        var serviceMock = new DeliveryServiceMock();
        var command = serviceMock.MockCommand();
        var service = serviceMock.MockDeliveryService();
        //Act
        service.Add(command);
        //Assert
        Assert.NotNull(serviceMock.OutPutDomainEvents[0] as DeliveryCreated);
    }
    [Fact]
    [Trait("Category", "Update")]
    [Trait("Category", "Success")]

    public void Update_Command_Result()
    {
        //Arrange
        var serviceMock = new DeliveryServiceMock();
        var command = serviceMock.MockCommand();
        var service = serviceMock.MockDeliveryService();
        //Act
        service.Update(command);
        //Assert
        Assert.NotNull(serviceMock.OutPutDomainEvents[0] as DeliveryUpdated);
    }
    [Fact]
    [Trait("Category", "Delete")]
    [Trait("Category", "Success")]

    public void Delete_Command_Result()
    {
        //Arrange
        var serviceMock = new DeliveryServiceMock();
        var command = serviceMock.MockCommand();
        var service = serviceMock.MockDeliveryService();
        //Act
        service.Delete(command);
        //Assert
        Assert.NotNull(serviceMock.OutPutDomainEvents[0] as DeliveryDeleted);
    }
}