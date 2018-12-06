CREATE TABLE [dbo].[Account]
(
	[AccountId] INT NOT NULL IDENTITY(1,1),
	[Username] VARCHAR(250) NOT NULL,
	[Password] VARCHAR(250) NOT NULL,
	[Profile] INT NOT NULL,
	[Role] INT NOT NULL,
	[Token] VARCHAR(1000) NULL, 
    CONSTRAINT [pkAccount] PRIMARY KEY CLUSTERED ([AccountId] ASC),
	CONSTRAINT [fkAccountEmployee] FOREIGN KEY ([Profile]) REFERENCES [dbo].[Employee] ([EmployeeId])
)
