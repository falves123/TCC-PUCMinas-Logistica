CREATE TABLE [Customer] (
    [CustomerID] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [CPF] nvarchar(max) NULL,
    [phone] nvarchar(max) NULL,
    [email] nvarchar(max) NULL
    CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerID])
);
GO


