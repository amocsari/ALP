CREATE TABLE [dbo].[Section]
(
	[SectionId] INT NOT NULL IDENTITY(1,1),
    [SectionName] VARCHAR(250) NOT NULL,
	[DepartmentId] INT NOT NULL,
	[FloorId] INT NOT NULL,
	CONSTRAINT [pkSection] PRIMARY KEY CLUSTERED ([SectionId] ASC),
	CONSTRAINT [pkSectionDepartment] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([DepartmentId]),
	CONSTRAINT [pkSectionFloor] FOREIGN KEY ([FloorId]) REFERENCES [dbo].[Floor] ([FloorId])
)