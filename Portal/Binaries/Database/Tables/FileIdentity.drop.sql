IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] DROP CONSTRAINT [DF_FileIdentity_ObjectId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] DROP CONSTRAINT [DF_FileIdentity_RecordId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_LibraryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] DROP CONSTRAINT [DF_FileIdentity_LibraryId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_FilePath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] DROP CONSTRAINT [DF_FileIdentity_FilePath]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] DROP CONSTRAINT [DF_FileIdentity_Name]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FileIdentity]') AND type in (N'U'))
DROP TABLE [dbo].[FileIdentity]
GO
