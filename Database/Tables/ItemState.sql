﻿CREATE TABLE [dbo].[ItemState]
(
	[ItemStateId] INT NOT NULL IDENTITY(1,1),
    [ItemStateName] VARCHAR(250) NOT NULL,
	[Locked] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [pkItemState] PRIMARY KEY CLUSTERED ([ItemStateId] ASC)
)
