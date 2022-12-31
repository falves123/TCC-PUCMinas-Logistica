CREATE TABLE [Payment] (
    [PaymentID] uniqueidentifier NOT NULL,
    [CustomerName] nvarchar(max) NULL,
    [OrderID] uniqueidentifier NOT NULL,
    [Value] float NOT NULL,
    CONSTRAINT [PK_Payment] PRIMARY KEY ([PaymentID])
);
GO


