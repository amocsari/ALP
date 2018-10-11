CREATE TABLE [dbo].[ItemState]
(
	[ItemStateId] INT NOT NULL IDENTITY(1,1),
    [ItemStateName] VARCHAR(250) NOT NULL
	CONSTRAINT [pkItemState] PRIMARY KEY CLUSTERED ([ItemStateId] ASC)
)
