namespace Tests_Application.Product;
public class ProductServiceTest
{
    [Fact]
    [Trait("Category", "Add")]
    [Trait("Category", "Success")]

    public void Add_Command_Result()
    {
        //Arrange
        var serviceMock = new ProductServiceMock();
        var command = serviceMock.MockCommand();
        var service = serviceMock.MockProductService();
        //Act
        service.Add(command);
        //Assert
        Assert.NotNull(serviceMock.OutPutDomainEvents[0] as ProductCreated);
    }
    [Fact]
    [Trait("Category", "Update")]
    [Trait("Category", "Success")]

    public void Update_Command_Result()
    {
        //Arrange
        var serviceMock = new ProductServiceMock();
        var command = serviceMock.MockCommand();
        var service = serviceMock.MockProductService();
        //Act
        service.Update(command);
        //Assert
        Assert.NotNull(serviceMock.OutPutDomainEvents[0] as ProductUpdated);
    }
    [Fact]
    [Trait("Category", "Delete")]
    [Trait("Category", "Success")]

    public void Delete_Command_Result()
    {
        //Arrange
        var serviceMock = new ProductServiceMock();
        var command = serviceMock.MockCommand();
        var service = serviceMock.MockProductService();
        //Act
        service.Delete(command);
        //Assert
        Assert.NotNull(serviceMock.OutPutDomainEvents[0] as ProductDeleted);
    }
}