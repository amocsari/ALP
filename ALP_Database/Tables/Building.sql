﻿CREATE TABLE [dbo].[Building]
(
	[BuildingId] INT NOT NULL IDENTITY(1,1),
    [BuildingName] VARCHAR(250) NOT NULL
	CONSTRAINT [pkBuilding] PRIMARY KEY CLUSTERED ([BuildingId] ASC)
)