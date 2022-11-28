namespace Tests_Application.Customer;
public class CustomerServiceTest
{
    [Fact]
    [Trait("Category", "Add")]
    [Trait("Category", "Success")]

    public void Add_Command_Result()
    {
        //Arrange
        var serviceMock = new CustomerServiceMock();
        var command = serviceMock.MockCommand();
        var service = serviceMock.MockCustomerService();
        //Act
        service.Add(command);
        //Assert
        Assert.NotNull(serviceMock.OutPutDomainEvents[0] as CustomerCreated);
    }
    [Fact]
    [Trait("Category", "Update")]
    [Trait("Category", "Success")]

    public void Update_Command_Result()
    {
        //Arrange
        var serviceMock = new CustomerServiceMock();
        var command = serviceMock.MockCommand();
        var service = serviceMock.MockCustomerService();
        //Act
        service.Update(command);
        //Assert
        Assert.NotNull(serviceMock.OutPutDomainEvents[0] as CustomerUpdated);
    }
    [Fact]
    [Trait("Category", "Delete")]
    [Trait("Category", "Success")]

    public void Delete_Command_Result()
    {
        //Arrange
        var serviceMock = new CustomerServiceMock();
        var command = serviceMock.MockCommand();
        var service = serviceMock.MockCustomerService();
        //Act
        service.Delete(command);
        //Assert
        Assert.NotNull(serviceMock.OutPutDomainEvents[0] as CustomerDeleted);
    }
}