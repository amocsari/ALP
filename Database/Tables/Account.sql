CREATE TABLE [dbo].[Account]
(
	[AccountId] INT NOT NULL IDENTITY(1,1),
	[Username] VARCHAR(250) NOT NULL,
	[Password] VARCHAR(250) NOT NULL,
	[Profile] INT NOT NULL,
	[Role] INT NOT NULL,
	CONSTRAINT [pkAccount] PRIMARY KEY CLUSTERED ([AccountId] ASC),
	CONSTRAINT [fkAccountEmployee] FOREIGN KEY ([Profile]) REFERENCES [dbo].[Employee] ([EmployeeId]),
	CONSTRAINT [fkAccountRole] FOREIGN KEY ([Role]) REFERENCES [dbo].[Role] ([RoleId])
)
