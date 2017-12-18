SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebSiteIdentity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebSiteIdentity](
	[Id] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[HostName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UrlPath] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Port] [int] NOT NULL,
	[IPAddress] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RedirectUrl] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProtocolId] [int] NOT NULL,
 CONSTRAINT [PK_WebSiteIdentity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_SiteId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] ADD  CONSTRAINT [DF_WebSiteIdentity_SiteId]  DEFAULT ((-1)) FOR [SiteId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_HostName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] ADD  CONSTRAINT [DF_WebSiteIdentity_HostName]  DEFAULT ('') FOR [HostName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_UrlPath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] ADD  CONSTRAINT [DF_WebSiteIdentity_UrlPath]  DEFAULT ('') FOR [UrlPath]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_Port]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] ADD  CONSTRAINT [DF_WebSiteIdentity_Port]  DEFAULT ((80)) FOR [Port]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteIdentity_IPAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] ADD  CONSTRAINT [DF_WebSiteIdentity_IPAddress]  DEFAULT ('') FOR [IPAddress]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSiteId__RedirectUrl]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] ADD  CONSTRAINT [DF__WebSiteId__RedirectUrl]  DEFAULT ('') FOR [RedirectUrl]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSiteId_ProtocolId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSiteIdentity] ADD  CONSTRAINT [DF_WebSiteId_ProtocolId]  DEFAULT ((0)) FOR [ProtocolId]
END

GO
