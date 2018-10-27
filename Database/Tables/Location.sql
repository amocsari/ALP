CREATE TABLE [dbo].[Location]
(
	[LocationId] INT NOT NULL IDENTITY(1,1),
    [LocationName] VARCHAR(250) NOT NULL,
	[Locked] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [pkLocation] PRIMARY KEY CLUSTERED ([LocationId] ASC)
)