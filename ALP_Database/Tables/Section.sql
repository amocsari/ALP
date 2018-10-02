CREATE TABLE [dbo].[Section]
(
	[SectionId] INT NOT NULL IDENTITY(1,1),
    [SectionName] VARCHAR(250) NOT NULL
	CONSTRAINT [pkSection] PRIMARY KEY CLUSTERED ([SectionId] ASC)
)