CREATE TABLE [dbo].[ItemNature]
(
	[ItemNatureId] INT NOT NULL IDENTITY(1,1),
    [ItemNatureName] VARCHAR(250) NOT NULL,
	[Locked] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [pkItemNature] PRIMARY KEY CLUSTERED ([ItemNatureId] ASC)
)