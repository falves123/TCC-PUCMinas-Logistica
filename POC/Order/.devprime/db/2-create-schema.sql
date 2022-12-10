CREATE TABLE [Order] (
    [OrderID] uniqueidentifier NOT NULL,
    [CustomerName] nvarchar(max) NULL,
    [CustomerTaxID] nvarchar(max) NULL,
    [Total] float NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([OrderID])
);
GO


CREATE TABLE [Item] (
    [ItemID] uniqueidentifier NOT NULL,
    [OrderID] uniqueidentifier NOT NULL,
    [Description] nvarchar(max) NULL,
    [Amount] int NOT NULL,
    [SKU] nvarchar(max) NULL,
    [Price] float NOT NULL,
    CONSTRAINT [PK_Item] PRIMARY KEY ([ItemID]),
    CONSTRAINT [FK_Item_Order_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Order] ([OrderID]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_Item_OrderID] ON [Item] ([OrderID]);
GO


