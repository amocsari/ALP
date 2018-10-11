CREATE TABLE [dbo].[Location]
(
	[LocationId] INT NOT NULL IDENTITY(1,1),
    [LocationName] VARCHAR(250) NOT NULL
	CONSTRAINT [pkLocation] PRIMARY KEY CLUSTERED ([LocationId] ASC)
)