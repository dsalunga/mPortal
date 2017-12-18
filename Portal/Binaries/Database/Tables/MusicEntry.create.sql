SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MusicEntry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MusicEntry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MusicId] [int] NOT NULL,
	[EntryTypeId] [int] NOT NULL,
	[FileName] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Tags] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[FileSize] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__MusicId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] ADD  CONSTRAINT [DF__MusicEntr__MusicId]  DEFAULT ((-1)) FOR [MusicId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__EntryTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] ADD  CONSTRAINT [DF__MusicEntr__EntryTypeId]  DEFAULT ((-1)) FOR [EntryTypeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__FileName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] ADD  CONSTRAINT [DF__MusicEntr__FileName]  DEFAULT ('') FOR [FileName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntry__Tags]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] ADD  CONSTRAINT [DF__MusicEntry__Tags]  DEFAULT ('') FOR [Tags]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] ADD  CONSTRAINT [DF__MusicEntr__DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__FileSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] ADD  CONSTRAINT [DF__MusicEntr__FileSize]  DEFAULT ((0)) FOR [FileSize]
END

GO
