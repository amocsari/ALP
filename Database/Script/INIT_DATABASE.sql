INSERT INTO [dbo].[ItemState]
           ([ItemStateName]
           ,[Locked])
     VALUES
           ('Új', 0)
		  ,('Selejtezett', 0)
		  ,('Kiosztott', 0)
GO

INSERT INTO [dbo].[Employee]
           ([EmployeeName]
           ,[EmailAddress])
     VALUES
           ('Mocsári András'
           ,'mocsari.andras@nebih.gov.hu')
GO

INSERT INTO [dbo].[Account]
           ([Username]
           ,[Password]
           ,[EmployeeId]
           ,[RoleId])
     VALUES
           ('admin'
           ,'admin'
           ,1
           ,1)
GO