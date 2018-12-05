CREATE TABLE [dbo].[Operation]
(
	[OperationId] INT NOT NULL IDENTITY(1,1),
    [OperationType] INT NOT NULL,
	[ItemId] INT NOT NULL,
	[PayLoadId] INT,
	[Priority] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [pkOperation] PRIMARY KEY CLUSTERED ([OperationId] ASC),
	CONSTRAINT [fkOperationItem] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Item] ([ItemId]),
)