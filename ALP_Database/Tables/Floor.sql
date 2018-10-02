CREATE TABLE [dbo].[Floor]
(
	[FloorId] INT NOT NULL IDENTITY(1,1),
    [FloorName] VARCHAR(250) NOT NULL,
	[BuildingId] INT NOT NULL,
	[ValidFrom] datetime2 (0) GENERATED ALWAYS AS ROW START,
	[ValidTo] datetime2 (0) GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo]),
	CONSTRAINT [pkFloor] PRIMARY KEY CLUSTERED ([FloorId] ASC),
	CONSTRAINT [fkFloorBuilding] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Building] ([BuildingId])
)