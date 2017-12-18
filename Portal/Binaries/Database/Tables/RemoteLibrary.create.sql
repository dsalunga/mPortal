SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoteLibrary]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RemoteLibrary](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](300) COLLATE Latin1_General_CI_AI NOT NULL,
	[SourceTypeId] [int] NOT NULL,
	[BaseAddress] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[UserName] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[Password] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[LastIndexDate] [datetime] NOT NULL,
	[Active] [int] NOT NULL,
	[DisplayBaseAddress] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DownloadCountSince] [datetime] NOT NULL,
	[FileCacheEnabled] [int] NOT NULL,
	[FileCacheFolder] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileCacheMinDownloadCount] [int] NOT NULL,
	[FileCacheCeilingSize] [int] NOT NULL,
	[FileCacheMaxSize] [int] NOT NULL,
	[FileCacheMinDiskFreeMB] [int] NOT NULL,
	[Size] [bigint] NOT NULL,
 CONSTRAINT [PK_RemoteLibrary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_SourceTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_SourceTypeId]  DEFAULT ((0)) FOR [SourceTypeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_BaseAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_BaseAddress]  DEFAULT ('') FOR [BaseAddress]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_UserName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_UserName]  DEFAULT ('') FOR [UserName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_Password]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_Password]  DEFAULT ('') FOR [Password]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_LastIndexDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_LastIndexDate]  DEFAULT (getdate()) FOR [LastIndexDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_Active]  DEFAULT ((1)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_DisplayBaseAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_DisplayBaseAddress]  DEFAULT ('') FOR [DisplayBaseAddress]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_DownloadCountSince]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_DownloadCountSince]  DEFAULT (getdate()) FOR [DownloadCountSince]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheEnabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_FileCacheEnabled]  DEFAULT ((0)) FOR [FileCacheEnabled]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheFolder]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_FileCacheFolder]  DEFAULT ('') FOR [FileCacheFolder]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheMinDwldCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_FileCacheMinDwldCount]  DEFAULT ((-1)) FOR [FileCacheMinDownloadCount]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheCeilSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_FileCacheCeilSize]  DEFAULT ((-1)) FOR [FileCacheCeilingSize]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheMaxSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_FileCacheMaxSize]  DEFAULT ((-1)) FOR [FileCacheMaxSize]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_FileCacheMinDiskFree]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_FileCacheMinDiskFree]  DEFAULT ((-1)) FOR [FileCacheMinDiskFreeMB]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RemoteLibrary_Size]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RemoteLibrary] ADD  CONSTRAINT [DF_RemoteLibrary_Size]  DEFAULT ((0)) FOR [Size]
END

GO
