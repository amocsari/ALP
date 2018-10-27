CREATE TABLE [dbo].[Department]
(
	[DepartmentId] INT NOT NULL IDENTITY(1,1),
    [DepartmentName] VARCHAR(250) NOT NULL,
	[DepartmentChief] INT NOT NULL,
	[Locked] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [pkDepartment] PRIMARY KEY CLUSTERED ([DepartmentId] ASC),
	CONSTRAINT [pkDepartmentUser] FOREIGN KEY ([DepartmentChief]) REFERENCES [dbo].[Employee] ([EmployeeId])
)