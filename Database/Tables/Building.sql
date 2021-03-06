﻿CREATE TABLE [dbo].[Building]
(
	[BuildingId] INT NOT NULL IDENTITY(1,1),
    [BuildingName] VARCHAR(250) NOT NULL,
	[LocationId] INT NOT NULL,
	[Locked] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [pkBuilding] PRIMARY KEY CLUSTERED ([BuildingId] ASC),
	CONSTRAINT [fkBuildingLocation] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([LocationId])
)