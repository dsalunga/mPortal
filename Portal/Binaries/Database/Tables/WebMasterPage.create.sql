SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebMasterPage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebMasterPage](
	[MasterPageId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[TemplateId] [int] NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnerPageId] [int] NOT NULL,
	[PublicAccess] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[ManagementAccess] [int] NOT NULL,
	[ThemeId] [int] NOT NULL,
	[SkinId] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
 CONSTRAINT [PK_WebMasterPages] PRIMARY KEY CLUSTERED 
(
	[MasterPageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPages_OwnerPageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] ADD  CONSTRAINT [DF_WebMasterPages_OwnerPageId]  DEFAULT ((-1)) FOR [OwnerPageId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPages_AuthenticationTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] ADD  CONSTRAINT [DF_WebMasterPages_AuthenticationTypeId]  DEFAULT ((1)) FOR [PublicAccess]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPages_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] ADD  CONSTRAINT [DF_WebMasterPages_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPages_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] ADD  CONSTRAINT [DF_WebMasterPages_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPage_ManagementAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] ADD  CONSTRAINT [DF_WebMasterPage_ManagementAccess]  DEFAULT ((0)) FOR [ManagementAccess]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPage_ThemeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] ADD  CONSTRAINT [DF_WebMasterPage_ThemeId]  DEFAULT ((-1)) FOR [ThemeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPage__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] ADD  CONSTRAINT [DF_WebMasterPage__SkinId]  DEFAULT ((-1)) FOR [SkinId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMasterPage_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMasterPage] ADD  CONSTRAINT [DF_WebMasterPage_ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
