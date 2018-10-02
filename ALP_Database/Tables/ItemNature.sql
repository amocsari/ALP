CREATE TABLE [dbo].[ItemNature]
(
	[ItemNatureId] INT NOT NULL IDENTITY(1,1),
    [ItemNatureName] VARCHAR(250) NOT NULL
	CONSTRAINT [pkItemNature] PRIMARY KEY CLUSTERED ([ItemNatureId] ASC)
)