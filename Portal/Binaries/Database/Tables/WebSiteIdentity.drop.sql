IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_SiteId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] DROP CONSTRAINT [DF_WebSiteIdentity_SiteId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_HostName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] DROP CONSTRAINT [DF_WebSiteIdentity_HostName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_UrlPath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] DROP CONSTRAINT [DF_WebSiteIdentity_UrlPath]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_Port]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] DROP CONSTRAINT [DF_WebSiteIdentity_Port]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_IPAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] DROP CONSTRAINT [DF_WebSiteIdentity_IPAddress]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSiteId__RedirectUrl]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] DROP CONSTRAINT [DF__WebSiteId__RedirectUrl]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteId_ProtocolId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] DROP CONSTRAINT [DF_WebSiteId_ProtocolId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebSiteIdentity]') AND type in (N'U'))
DROP TABLE [dbo].[WebSiteIdentity]
GO
