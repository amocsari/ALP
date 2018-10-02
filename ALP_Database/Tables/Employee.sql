CREATE TABLE [dbo].[Employee]
(
	[EmployeeId] INT NOT NULL IDENTITY(1,1),
    [EmployeeName] VARCHAR(250) NOT NULL,
	[Department] INT NOT NULL,
	[Section] INT,
	[EmailAddress] VARCHAR(100),
	--TODO: fancy-bb név
	[RetireDate] DATE,
	CONSTRAINT [pkEmployee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
	CONSTRAINT [fkEmployeeDepartment] FOREIGN KEY ([Department]) REFERENCES [dbo].[Department] ([DepartmentId]),
	CONSTRAINT [fkEmployeeSection] FOREIGN KEY ([Section]) REFERENCES [dbo].[Section] ([SectionId]),
)