namespace Tests_Domain.Delivery;
public partial class DeliveryAggRootTest
{
#region ValidateFields
    [Fact]
    [Trait("Category", "Validate")]
    [Trait("Category", "Success")]

    public void ValidateFields_FieldsAreValid_Success()
    {
        //Arrange
        var agg = MockDelivery();
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

    public void Add_AllFieldsFilled_TriggerEventDeliveryCreated()
    {
        //Arrange
        var agg = MockDelivery();
        //Act
        agg.Add();
        //Assert
        Assert.True(agg.Dp.GetDomainEvent() is DeliveryCreated);
    }

#endregion Add

#region Update
    [Fact]
    [Trait("Category", "Update")]
    [Trait("Category", "Success")]

    public void Update_FieldsFilled_TriggerEventDeliveryUpdated()
    {
        //Arrange
        var agg = MockDelivery();
        //Act
        agg.Update();
        //Assert
        Assert.True(agg.Dp.GetDomainEvent() is DeliveryUpdated);
    }

#endregion Update

#region Delete
    [Fact]
    [Trait("Category", "Delete")]
    [Trait("Category", "Success")]

    public void Delete_FieldsFilled_TriggerEventDeliveryDeleted()
    {
        //Arrange
        var agg = MockDelivery();
        agg.ID = Guid.NewGuid();
        //Act
        agg.Delete();
        //Assert
        Assert.True(agg.Dp.GetDomainEvent() is DeliveryDeleted);
    }
    [Fact]
    [Trait("Category", "Delete")]
    [Trait("Category", "Failure")]

    public void Delete_FieldsNotFilled_DontTriggerEvent()
    {
        //Arrange
        var agg = MockDelivery();
        agg.ID = Guid.Empty;
        //Act
        agg.Delete();
        //Assert
        Assert.False(agg.Dp.GetDomainEvent() is DeliveryDeleted);
    }

#endregion Delete
}