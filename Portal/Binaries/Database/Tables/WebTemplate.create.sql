SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebTemplate](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Identity] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PrimaryPanelId] [int] NOT NULL,
	[Version] [int] NOT NULL,
	[VersionOf] [int] NOT NULL,
	[Content] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[ThemeId] [int] NOT NULL,
	[Standalone] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[SkinId] [int] NOT NULL,
	[TemplateEngineId] [int] NOT NULL,
 CONSTRAINT [PK_WebTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplates_PrimaryPanelId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] ADD  CONSTRAINT [DF_WebTemplates_PrimaryPanelId]  DEFAULT ((-1)) FOR [PrimaryPanelId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplate_Version]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] ADD  CONSTRAINT [DF_WebTemplate_Version]  DEFAULT ((1)) FOR [Version]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplate_LatestVersion]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] ADD  CONSTRAINT [DF_WebTemplate_LatestVersion]  DEFAULT ((1)) FOR [VersionOf]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplate_Content]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] ADD  CONSTRAINT [DF_WebTemplate_Content]  DEFAULT ('') FOR [Content]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplate_SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] ADD  CONSTRAINT [DF_WebTemplate_SkinId]  DEFAULT ((-1)) FOR [ThemeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__Standalone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] ADD  CONSTRAINT [DF__WebTempla__Standalone]  DEFAULT ((0)) FOR [Standalone]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] ADD  CONSTRAINT [DF__WebTempla__ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] ADD  CONSTRAINT [DF__WebTempla__SkinId]  DEFAULT ((-1)) FOR [SkinId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__TemplateEngineId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplate] ADD  CONSTRAINT [DF__WebTempla__TemplateEngineId]  DEFAULT ((1)) FOR [TemplateEngineId]
END

GO
