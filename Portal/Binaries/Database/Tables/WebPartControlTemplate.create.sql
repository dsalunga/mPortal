SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPartControlTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebPartControlTemplate](
	[PartControlTemplateId] [int] NOT NULL,
	[PartControlId] [int] NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Identity] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Path] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Standalone] [int] NOT NULL,
	[TemplateEngineId] [int] NOT NULL,
 CONSTRAINT [PK_WebPartControlTemplates] PRIMARY KEY CLUSTERED 
(
	[PartControlTemplateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartControlTemplates_CompletePath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControlTemplate] ADD  CONSTRAINT [DF_WebPartControlTemplates_CompletePath]  DEFAULT ('') FOR [Path]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPartControlTemplate__Standalone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControlTemplate] ADD  CONSTRAINT [DF__WebPartControlTemplate__Standalone]  DEFAULT ((0)) FOR [Standalone]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartControlTemplate_EngineId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControlTemplate] ADD  CONSTRAINT [DF_WebPartControlTemplate_EngineId]  DEFAULT ((1)) FOR [TemplateEngineId]
END

GO
