CREATE TABLE [Delivery] (
    [DeliveryID] uniqueidentifier NOT NULL,
    [Started] datetime2 NOT NULL,
    [Finished] datetime2 NOT NULL,
    [OrderID] uniqueidentifier NOT NULL,
    [Total] float NOT NULL,
    CONSTRAINT [PK_Delivery] PRIMARY KEY ([DeliveryID])
);
GO


CREATE TABLE [Product] (
    [ProductID] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Sku] nvarchar(max) NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductID])
);
GO


