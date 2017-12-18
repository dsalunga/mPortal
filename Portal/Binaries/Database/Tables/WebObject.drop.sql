IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_ObjectType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObjects_ObjectType]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_Prefix]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObject_Prefix]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_LastRecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObjects_LastRecordId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_MaxCacheSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObjects_MaxCacheSize]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_AccessTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObjects_AccessTypeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_CacheTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObjects_CacheTypeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_MaxHistorySize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObjects_MaxHistorySize]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_DataProviderName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObject_DataProviderName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_TypeName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObject_TypeName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_CacheInterval]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObject_CacheInterval]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObject_DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_ManagerName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObject_ManagerName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_NameColumn]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObject_NameColumn]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_FriendlyName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] DROP CONSTRAINT [DF_WebObject_FriendlyName]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObject]') AND type in (N'U'))
DROP TABLE [dbo].[WebObject]
GO
