SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTemplatePanel]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebTemplatePanel](
	[TemplatePanelId] [int] NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TemplateId] [int] NOT NULL,
	[PanelName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Rank] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
 CONSTRAINT [PK_WebTemplatePanels] PRIMARY KEY CLUSTERED 
(
	[TemplatePanelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplatePanels_TemplateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplatePanel] ADD  CONSTRAINT [DF_WebTemplatePanels_TemplateId]  DEFAULT ((-1)) FOR [TemplateId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTemplatePanel_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplatePanel] ADD  CONSTRAINT [DF_WebTemplatePanel_Rank]  DEFAULT ((0)) FOR [Rank]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplatePanel] ADD  CONSTRAINT [DF__WebTempla__ObjectId]  DEFAULT ((-1)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebTempla__RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTemplatePanel] ADD  CONSTRAINT [DF__WebTempla__RecordId]  DEFAULT ((-1)) FOR [RecordId]
END

GO
