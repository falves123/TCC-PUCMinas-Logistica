namespace Tests_Domain.Product;
public partial class ProductAggRootTest
{
#region ValidateFields
    [Fact]
    [Trait("Category", "Validate")]
    [Trait("Category", "Success")]

    public void ValidateFields_FieldsAreValid_Success()
    {
        //Arrange
        var agg = MockProduct();
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

    public void Add_AllFieldsFilled_TriggerEventProductCreated()
    {
        //Arrange
        var agg = MockProduct();
        //Act
        agg.Add();
        //Assert
        Assert.True(agg.Dp.GetDomainEvent() is ProductCreated);
    }

#endregion Add

#region Update
    [Fact]
    [Trait("Category", "Update")]
    [Trait("Category", "Success")]

    public void Update_FieldsFilled_TriggerEventProductUpdated()
    {
        //Arrange
        var agg = MockProduct();
        //Act
        agg.Update();
        //Assert
        Assert.True(agg.Dp.GetDomainEvent() is ProductUpdated);
    }

#endregion Update

#region Delete
    [Fact]
    [Trait("Category", "Delete")]
    [Trait("Category", "Success")]

    public void Delete_FieldsFilled_TriggerEventProductDeleted()
    {
        //Arrange
        var agg = MockProduct();
        agg.ID = Guid.NewGuid();
        //Act
        agg.Delete();
        //Assert
        Assert.True(agg.Dp.GetDomainEvent() is ProductDeleted);
    }
    [Fact]
    [Trait("Category", "Delete")]
    [Trait("Category", "Failure")]

    public void Delete_FieldsNotFilled_DontTriggerEvent()
    {
        //Arrange
        var agg = MockProduct();
        agg.ID = Guid.Empty;
        //Act
        agg.Delete();
        //Assert
        Assert.False(agg.Dp.GetDomainEvent() is ProductDeleted);
    }

#endregion Delete
}