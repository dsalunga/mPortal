IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderVersionAccess_BibleVersionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderVersionAccess] DROP CONSTRAINT [DF_BibleReaderVersionAccess_BibleVersionId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderVersionAccess_BibleVersionName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderVersionAccess] DROP CONSTRAINT [DF_BibleReaderVersionAccess_BibleVersionName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderVersionAccess_LastAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderVersionAccess] DROP CONSTRAINT [DF_BibleReaderVersionAccess_LastAccess]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderVersionAccess_VersionAccessCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderVersionAccess] DROP CONSTRAINT [DF_BibleReaderVersionAccess_VersionAccessCount]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BibleReaderVersionAccess]') AND type in (N'U'))
DROP TABLE [dbo].[BibleReaderVersionAccess]
GO
