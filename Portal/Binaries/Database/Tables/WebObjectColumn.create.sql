SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObjectColumn]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebObjectColumn](
	[Id] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectColumn_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectColumn] ADD  CONSTRAINT [DF_WebObjectColumn_Id]  DEFAULT ((-1)) FOR [Id]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectColumn_ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectColumn] ADD  CONSTRAINT [DF_WebObjectColumn_ObjectId]  DEFAULT ((-1)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectColumn_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectColumn] ADD  CONSTRAINT [DF_WebObjectColumn_Name]  DEFAULT ('') FOR [Name]
END

GO
