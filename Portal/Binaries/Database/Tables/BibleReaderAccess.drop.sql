IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderAccess_AppAccessCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderAccess] DROP CONSTRAINT [DF_BibleReaderAccess_AppAccessCount]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderAccess_LastAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderAccess] DROP CONSTRAINT [DF_BibleReaderAccess_LastAccess]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BibleReaderAccess]') AND type in (N'U'))
DROP TABLE [dbo].[BibleReaderAccess]
GO
