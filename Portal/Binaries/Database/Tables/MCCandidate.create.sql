SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCCandidate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MCCandidate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Entry] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Lyrics] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SourceUrl] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SourceUrl2] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Lyricist] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Interpreter] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PhotoFile] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[CompetitionId] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[WinnerRank] [int] NOT NULL,
 CONSTRAINT [PK_MCCandidate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Entry]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_Entry]  DEFAULT ('') FOR [Entry]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Lyrics]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_Lyrics]  DEFAULT ('') FOR [Lyrics]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_SourceUrl]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_SourceUrl]  DEFAULT ('') FOR [SourceUrl]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_SourceUrl2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_SourceUrl2]  DEFAULT ('') FOR [SourceUrl2]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Lyricist]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_Lyricist]  DEFAULT ('') FOR [Lyricist]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Interpreter]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_Interpreter]  DEFAULT ('') FOR [Interpreter]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_PhotoFile]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_PhotoFile]  DEFAULT ('') FOR [PhotoFile]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_CompetitionId]  DEFAULT ((-1)) FOR [CompetitionId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_Rank]  DEFAULT ((0)) FOR [Rank]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_WinnerRank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] ADD  CONSTRAINT [DF_MCCandidate_WinnerRank]  DEFAULT ((0)) FOR [WinnerRank]
END

GO
