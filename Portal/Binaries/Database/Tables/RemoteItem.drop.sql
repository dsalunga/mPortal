IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_LibraryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_LibraryId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_RelativePath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_RelativePath]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_TypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_TypeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_Size]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_Size]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_Content]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_Content]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_ParentId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_DownloadCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_DownloadCount]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_DisplayName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_DisplayName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_IdxDateMdf]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItems_IdxDateMdf]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItem_FileCachedEnabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItem_FileCachedEnabled]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItem_Cached]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] DROP CONSTRAINT [DF_RemoteItem_Cached]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoteItem]') AND type in (N'U'))
DROP TABLE [dbo].[RemoteItem]
GO
