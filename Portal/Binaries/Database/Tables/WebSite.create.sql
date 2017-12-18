SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebSite]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebSite](
	[SiteId] [int] NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Rank] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[Identity] [nvarchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ParentId] [int] NOT NULL,
	[HomePageId] [int] NOT NULL,
	[DefaultMasterPageId] [int] NOT NULL,
	[HostName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Published] [int] NOT NULL,
	[VersionOf] [int] NOT NULL,
	[PublicAccess] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[LoginPage] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AccessDeniedPage] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PageTitleFormat] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[ManagementAccess] [int] NOT NULL,
	[BaseAddress] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ThemeId] [int] NOT NULL,
	[LocaleId] [int] NOT NULL,
	[SkinId] [int] NOT NULL,
	[PrimaryIdentityId] [int] NOT NULL,
 CONSTRAINT [PK_WebSites] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_Published]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSites_Published]  DEFAULT ((-1)) FOR [Published]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_VersionOfId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSites_VersionOfId]  DEFAULT ((-1)) FOR [VersionOf]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_AuthenticationTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSites_AuthenticationTypeId]  DEFAULT ((128)) FOR [PublicAccess]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSites_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSites_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_LoginPage]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSites_LoginPage]  DEFAULT ('') FOR [LoginPage]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSites_AccessDeniedPage]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSites_AccessDeniedPage]  DEFAULT ('') FOR [AccessDeniedPage]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSite_PageTitleFormat]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSite_PageTitleFormat]  DEFAULT ('') FOR [PageTitleFormat]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSite_ManagementAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSite_ManagementAccess]  DEFAULT ((0)) FOR [ManagementAccess]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSite_BaseAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSite_BaseAddress]  DEFAULT ('') FOR [BaseAddress]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSite__ThemeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF__WebSite__ThemeId]  DEFAULT ((-1)) FOR [ThemeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSite__LocaleId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF__WebSite__LocaleId]  DEFAULT ((-1)) FOR [LocaleId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSite__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF__WebSite__SkinId]  DEFAULT ((-1)) FOR [SkinId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSite_PrimaryIdentityId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSite] ADD  CONSTRAINT [DF_WebSite_PrimaryIdentityId]  DEFAULT ((-1)) FOR [PrimaryIdentityId]
END

GO
