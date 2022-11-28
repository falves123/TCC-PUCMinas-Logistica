namespace Tests_Domain.Customer;
public partial class CustomerAggRootTest
{
#region ValidateFields
    [Fact]
    [Trait("Category", "Validate")]
    [Trait("Category", "Success")]

    public void ValidateFields_FieldsAreValid_Success()
    {
        //Arrange
        var agg = MockCustomer();
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

    public void Add_AllFieldsFilled_TriggerEventCustomerCreated()
    {
        //Arrange
        var agg = MockCustomer();
        //Act
        agg.Add();
        //Assert
        Assert.True(agg.Dp.GetDomainEvent() is CustomerCreated);
    }

#endregion Add

#region Update
    [Fact]
    [Trait("Category", "Update")]
    [Trait("Category", "Success")]

    public void Update_FieldsFilled_TriggerEventCustomerUpdated()
    {
        //Arrange
        var agg = MockCustomer();
        //Act
        agg.Update();
        //Assert
        Assert.True(agg.Dp.GetDomainEvent() is CustomerUpdated);
    }

#endregion Update

#region Delete
    [Fact]
    [Trait("Category", "Delete")]
    [Trait("Category", "Success")]

    public void Delete_FieldsFilled_TriggerEventCustomerDeleted()
    {
        //Arrange
        var agg = MockCustomer();
        agg.ID = Guid.NewGuid();
        //Act
        agg.Delete();
        //Assert
        Assert.True(agg.Dp.GetDomainEvent() is CustomerDeleted);
    }
    [Fact]
    [Trait("Category", "Delete")]
    [Trait("Category", "Failure")]

    public void Delete_FieldsNotFilled_DontTriggerEvent()
    {
        //Arrange
        var agg = MockCustomer();
        agg.ID = Guid.Empty;
        //Act
        agg.Delete();
        //Assert
        Assert.False(agg.Dp.GetDomainEvent() is CustomerDeleted);
    }

#endregion Delete
}