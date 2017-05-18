CREATE TABLE [dbo].[Order]
(
	[OrderID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Product] VARCHAR(50) NOT NULL, 
    [Price] MONEY NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT (1), 
    [Date] DATETIME2 NOT NULL DEFAULT (GETDATE()), 
    [CustomerID] INT NOT NULL, 
    [FulFilled] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Order_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer]([CustomerID])
)
