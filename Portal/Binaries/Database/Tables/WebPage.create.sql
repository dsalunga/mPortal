SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebPage](
	[PageId] [int] NOT NULL,
	[Name] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SiteId] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[Identity] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ParentId] [int] NOT NULL,
	[Title] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MasterPageId] [int] NOT NULL,
	[PartControlTemplateId] [int] NOT NULL,
	[Published] [int] NOT NULL,
	[VersionOfId] [int] NOT NULL,
	[PublicAccess] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[PageType] [int] NOT NULL,
	[UsePartTemplatePath] [int] NOT NULL,
	[ManagementAccess] [int] NOT NULL,
	[ThemeId] [int] NOT NULL,
	[LocaleId] [int] NOT NULL,
	[SkinId] [int] NOT NULL,
 CONSTRAINT [PK_WebPages] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_PartControlTemplateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPages_PartControlTemplateId]  DEFAULT ((-1)) FOR [PartControlTemplateId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_Published]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPages_Published]  DEFAULT ((-1)) FOR [Published]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_VersionOfId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPages_VersionOfId]  DEFAULT ((-1)) FOR [VersionOfId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_AuthMethodId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPages_AuthMethodId]  DEFAULT ((128)) FOR [PublicAccess]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPages_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPages_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_PageType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPages_PageType]  DEFAULT ((0)) FOR [PageType]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPages_UseTemplatePath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPages_UseTemplatePath]  DEFAULT ((1)) FOR [UsePartTemplatePath]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPage_ManagementAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPage_ManagementAccess]  DEFAULT ((0)) FOR [ManagementAccess]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPage_ThemeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF_WebPage_ThemeId]  DEFAULT ((-1)) FOR [ThemeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPage__LocaleId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF__WebPage__LocaleId]  DEFAULT ((-1)) FOR [LocaleId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPage__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPage] ADD  CONSTRAINT [DF__WebPage__SkinId]  DEFAULT ((-1)) FOR [SkinId]
END

GO
