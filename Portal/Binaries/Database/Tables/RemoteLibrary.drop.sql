IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_SourceTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_SourceTypeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_BaseAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_BaseAddress]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_UserName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_UserName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_Password]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_Password]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_LastIndexDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_LastIndexDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_Active]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_DisplayBaseAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_DisplayBaseAddress]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_DownloadCountSince]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_DownloadCountSince]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheEnabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_FileCacheEnabled]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheFolder]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_FileCacheFolder]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheMinDwldCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_FileCacheMinDwldCount]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheCeilSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_FileCacheCeilSize]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheMaxSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_FileCacheMaxSize]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheMinDiskFree]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_FileCacheMinDiskFree]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_Size]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] DROP CONSTRAINT [DF_RemoteLibrary_Size]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoteLibrary]') AND type in (N'U'))
DROP TABLE [dbo].[RemoteLibrary]
GO
