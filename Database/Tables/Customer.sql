CREATE TABLE [dbo].[Customer]
(
	[CustomerID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [FirstName] VARCHAR(300) NOT NULL, 
    [LastName] VARCHAR(300) NOT NULL, 
    [Email] VARCHAR(300) NULL, 
    [Address] VARCHAR(300) NULL, 
    [City] VARCHAR(300) NULL, 
    [StateID] INT NULL, 
    [Zip] VARCHAR(10) NULL, 
    [GenderID] INT NULL, 
    CONSTRAINT [FK_Customer_State] FOREIGN KEY ([StateID]) REFERENCES [dbo].[State]([StateID]),
	CONSTRAINT [FK_Customer_Gender] FOREIGN KEY ([GenderID]) REFERENCES [dbo].[Gender]([GenderID])
)
