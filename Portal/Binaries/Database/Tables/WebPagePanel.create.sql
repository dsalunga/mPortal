SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPagePanel]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebPagePanel](
	[PagePanelId] [int] NOT NULL,
	[TemplatePanelId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[UsageTypeId] [int] NOT NULL,
 CONSTRAINT [PK_WebPagePanels] PRIMARY KEY CLUSTERED 
(
	[PagePanelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPagePanels_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPagePanel] ADD  CONSTRAINT [DF_WebPagePanels_PageId]  DEFAULT ((-1)) FOR [PageId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPagePanels_UsageTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPagePanel] ADD  CONSTRAINT [DF_WebPagePanels_UsageTypeId]  DEFAULT ((-1)) FOR [UsageTypeId]
END

GO
