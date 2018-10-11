CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL IDENTITY(1,1),
	[Username] VARCHAR(250) NOT NULL,
	[Password] VARCHAR(250) NOT NULL,
	[Profile] INT NOT NULL,
	[Role] INT NOT NULL,
	CONSTRAINT [pkUser] PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [fkUserEmployee] FOREIGN KEY ([Profile]) REFERENCES [dbo].[Employee] ([EmployeeId]),
	CONSTRAINT [fkUserRole] FOREIGN KEY ([Role]) REFERENCES [dbo].[Role] ([RoleId])
)
