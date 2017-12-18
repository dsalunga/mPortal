IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Menu_RefPageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Menu] DROP CONSTRAINT [DF_Menu_RefPageId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Menu_IncludeChildren]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Menu] DROP CONSTRAINT [DF_Menu_IncludeChildren]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
DROP TABLE [dbo].[Menu]
GO
