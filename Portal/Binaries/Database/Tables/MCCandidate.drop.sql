IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Entry]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_Entry]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Lyrics]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_Lyrics]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_SourceUrl]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_SourceUrl]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_SourceUrl2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_SourceUrl2]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Lyricist]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_Lyricist]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Interpreter]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_Interpreter]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_PhotoFile]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_PhotoFile]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_CompetitionId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_Rank]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCCandidate_WinnerRank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCCandidate] DROP CONSTRAINT [DF_MCCandidate_WinnerRank]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCCandidate]') AND type in (N'U'))
DROP TABLE [dbo].[MCCandidate]
GO
