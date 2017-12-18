IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResources_DirectoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] DROP CONSTRAINT [DF_WebTextResources_DirectoryId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResources_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] DROP CONSTRAINT [DF_WebTextResources_Rank]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] DROP CONSTRAINT [DF_WebTextResource_DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_OwnerObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] DROP CONSTRAINT [DF_WebTextResource_OwnerObjectId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_OwnerRecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] DROP CONSTRAINT [DF_WebTextResource_OwnerRecordId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_DatePersisted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] DROP CONSTRAINT [DF_WebTextResource_DatePersisted]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_PhysicalPath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] DROP CONSTRAINT [DF_WebTextResource_PhysicalPath]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTextResource]') AND type in (N'U'))
DROP TABLE [dbo].[WebTextResource]
GO
