CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL IDENTITY(1,1),
    [UserName] VARCHAR(250) NOT NULL,
	[DepartmentId] INT NOT NULL,
	CONSTRAINT [pkUser] PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [fkUserDepartment] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([DepartmentId])
)