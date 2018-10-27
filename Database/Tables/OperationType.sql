CREATE TABLE [dbo].[OperationType]
(
	[OperationTypeId] INT NOT NULL IDENTITY(1,1),
    [OperationTypeName] VARCHAR(250) NOT NULL,
	[Locked] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [pkOperationType] PRIMARY KEY CLUSTERED ([OperationTypeId] ASC)
)