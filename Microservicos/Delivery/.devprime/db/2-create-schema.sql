CREATE TABLE [Product] (
    [ProductID] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Sku] nvarchar(max) NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductID])
);
GO


