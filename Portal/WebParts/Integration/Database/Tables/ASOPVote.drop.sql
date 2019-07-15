IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_Code]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF_MCVote_Code]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_FirstName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF_MCVote_FirstName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_LastName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF_MCVote_LastName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_MobileNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF_MCVote_MobileNumber]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_Email]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF_MCVote_Email]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_CandidateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF_MCVote_CandidateId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_UserName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF_MCVote_UserName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCVote__Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF__MCVote__Status]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCVote__CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF__MCVote__CompetitionId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCVote__IPAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF__MCVote__IPAddress]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCVote__Spam]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] DROP CONSTRAINT [DF__MCVote__Spam]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCVote]') AND type in (N'U'))
DROP TABLE [dbo].[MCVote]
GO
