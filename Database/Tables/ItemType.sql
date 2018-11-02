CREATE TABLE [dbo].[ItemType]
(
	[ItemTypeId] INT NOT NULL IDENTITY(1,1),
    [ItemTypeName] VARCHAR(250) NOT NULL,
	[ItemNatureId] INT NOT NULL,
	[Locked] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [pkItemType] PRIMARY KEY CLUSTERED ([ItemTypeId] ASC),
	CONSTRAINT [fkItemTypeItemNature] FOREIGN KEY ([ItemNatureId]) REFERENCES [dbo].[ItemNature] ([ItemNatureId])
)