CREATE TABLE [dbo].[Employee]
(
	[EmployeeId] INT NOT NULL IDENTITY(1,1),
    [EmployeeName] VARCHAR(250) NOT NULL,
	[DepartmentId] INT,
	[SectionId] INT,
	[EmailAddress] VARCHAR(100),
	[RetirementDate] DATE,
	CONSTRAINT [pkEmployee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
	CONSTRAINT [fkEmployeeDepartment] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([DepartmentId]),
	CONSTRAINT [fkEmployeeSection] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([SectionId]),
)