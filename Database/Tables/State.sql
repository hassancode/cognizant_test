CREATE TABLE [dbo].[State]
(
	[StateID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Abbreviation] CHAR(2) NOT NULL, 
    [Name] VARCHAR(50) NOT NULL
)
