SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPageElement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebPageElement](
	[PageElementId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TemplatePanelId] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[PartControlTemplateId] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[UsePartTemplatePath] [int] NOT NULL,
	[PublicAccess] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[ManagementAccess] [int] NOT NULL,
 CONSTRAINT [PK_WebMasterPageItems] PRIMARY KEY CLUSTERED 
(
	[PageElementId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_ObjectId_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] ADD  CONSTRAINT [DF_WebPageElement_ObjectId_1]  DEFAULT ((2)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_UsePartTemplatePath_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] ADD  CONSTRAINT [DF_WebPageElement_UsePartTemplatePath_1]  DEFAULT ((1)) FOR [UsePartTemplatePath]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_PublicAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] ADD  CONSTRAINT [DF_WebPageElement_PublicAccess]  DEFAULT ((1)) FOR [PublicAccess]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] ADD  CONSTRAINT [DF_WebPageElement_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_ManagementAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] ADD  CONSTRAINT [DF_WebPageElement_ManagementAccess]  DEFAULT ((0)) FOR [ManagementAccess]
END

GO
