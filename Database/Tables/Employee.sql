CREATE TABLE [dbo].[Employee]
(
	[EmployeeId] INT NOT NULL IDENTITY(1,1),
    [EmployeeName] VARCHAR(250) NOT NULL,
	[DepartmentId] INT NOT NULL,
	[SectionId] INT,
	[EmailAddress] VARCHAR(100),
	--TODO: fancy-bb név
	[RetirementDate] DATE,
	CONSTRAINT [pkEmployee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
	CONSTRAINT [fkEmployeeDepartment] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([DepartmentId]),
	CONSTRAINT [fkEmployeeSection] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([SectionId]),
)