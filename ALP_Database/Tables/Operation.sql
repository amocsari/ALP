CREATE TABLE [dbo].[Operation]
(
	[OperationId] INT NOT NULL IDENTITY(1,1),
    [OperationType] INT NOT NULL,
	[TargetItem] INT NOT NULL,
	[PayLoad] INT,
	CONSTRAINT [pkOperation] PRIMARY KEY CLUSTERED ([OperationId] ASC),
	CONSTRAINT [fkOperationItem] FOREIGN KEY ([TargetItem]) REFERENCES [dbo].[Item] ([ItemId]),
	CONSTRAINT [fkOperationOperationType] FOREIGN KEY ([OperationType]) REFERENCES [dbo].[OperationType] ([OperationTypeId]),
)