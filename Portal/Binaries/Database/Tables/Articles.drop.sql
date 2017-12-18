IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Articles_DirectoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Articles] DROP CONSTRAINT [DF_Articles_DirectoryId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Articles_Tags]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Articles] DROP CONSTRAINT [DF_Articles_Tags]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Articles]') AND type in (N'U'))
DROP TABLE [dbo].[Articles]
GO
