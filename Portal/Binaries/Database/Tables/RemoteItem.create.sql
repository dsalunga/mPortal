SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoteItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RemoteItem](
	[Id] [int] NOT NULL,
	[LibraryId] [int] NOT NULL,
	[Name] [nvarchar](300) COLLATE Latin1_General_CI_AI NOT NULL,
	[RelativePath] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[TypeId] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[Size] [bigint] NOT NULL,
	[Content] [ntext] COLLATE Latin1_General_CI_AI NOT NULL,
	[ParentId] [int] NOT NULL,
	[DownloadCount] [int] NOT NULL,
	[DisplayName] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IndexDateModified] [datetime] NOT NULL,
	[FileCacheEnabled] [int] NOT NULL,
	[Cached] [int] NOT NULL,
 CONSTRAINT [PK_RemoteItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_LibraryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_LibraryId]  DEFAULT ((-1)) FOR [LibraryId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_RelativePath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_RelativePath]  DEFAULT ('') FOR [RelativePath]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_TypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_TypeId]  DEFAULT ((0)) FOR [TypeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_Size]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_Size]  DEFAULT ((0)) FOR [Size]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_Content]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_Content]  DEFAULT ('') FOR [Content]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_DownloadCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_DownloadCount]  DEFAULT ((0)) FOR [DownloadCount]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_DisplayName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_DisplayName]  DEFAULT ('') FOR [DisplayName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItems_IdxDateMdf]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItems_IdxDateMdf]  DEFAULT (getdate()) FOR [IndexDateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItem_FileCachedEnabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItem_FileCachedEnabled]  DEFAULT ((-1)) FOR [FileCacheEnabled]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteItem_Cached]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteItem] ADD  CONSTRAINT [DF_RemoteItem_Cached]  DEFAULT ((0)) FOR [Cached]
END

GO
