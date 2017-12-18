SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTheme]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebTheme](
	[Id] [int] NOT NULL,
	[TemplateId] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ParentId] [int] NOT NULL,
	[Identity] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SkinId] [int] NOT NULL,
 CONSTRAINT [PK_WebTheme] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTheme_TemplateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] ADD  CONSTRAINT [DF_WebTheme_TemplateId]  DEFAULT ((-1)) FOR [TemplateId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTheme_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] ADD  CONSTRAINT [DF_WebTheme_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTheme__ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] ADD  CONSTRAINT [DF__WebTheme__ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTheme__Identiy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] ADD  CONSTRAINT [DF__WebTheme__Identiy]  DEFAULT ('') FOR [Identity]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTheme__SkinId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTheme] ADD  CONSTRAINT [DF__WebTheme__SkinId]  DEFAULT ((-1)) FOR [SkinId]
END

GO
