SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebContent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebContent](
	[ContentId] [int] NOT NULL,
	[Title] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Content] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[VersionOf] [int] NOT NULL,
	[VersionNo] [int] NOT NULL,
	[DirectoryId] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[ContentTypeId] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[EditorSensitive] [int] NOT NULL,
	[ActiveContent] [int] NOT NULL,
 CONSTRAINT [PK_WebContents] PRIMARY KEY CLUSTERED 
(
	[ContentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContents_VersionOfId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] ADD  CONSTRAINT [DF_WebContents_VersionOfId]  DEFAULT ((-1)) FOR [VersionOf]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContents_VersionNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] ADD  CONSTRAINT [DF_WebContents_VersionNo]  DEFAULT ((-1)) FOR [VersionNo]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContents_DirectoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] ADD  CONSTRAINT [DF_WebContents_DirectoryId]  DEFAULT ((-1)) FOR [DirectoryId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContent_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] ADD  CONSTRAINT [DF_WebContent_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContent_ContentTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] ADD  CONSTRAINT [DF_WebContent_ContentTypeId]  DEFAULT ((4)) FOR [ContentTypeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContent_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] ADD  CONSTRAINT [DF_WebContent_Active]  DEFAULT ((1)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebContent_SiteId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] ADD  CONSTRAINT [DF_WebContent_SiteId]  DEFAULT ((-1)) FOR [SiteId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebConten__EditorSensitive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] ADD  CONSTRAINT [DF__WebConten__EditorSensitive]  DEFAULT ((0)) FOR [EditorSensitive]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebConten_ActiveContent]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebContent] ADD  CONSTRAINT [DF_WebConten_ActiveContent]  DEFAULT ((0)) FOR [ActiveContent]
END

GO
