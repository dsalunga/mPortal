IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCInter__DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCInterpreterScore] DROP CONSTRAINT [DF__MCInter__DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCInter__CandidateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCInterpreterScore] DROP CONSTRAINT [DF__MCInter__CandidateId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCInter__CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCInterpreterScore] DROP CONSTRAINT [DF__MCInter__CompetitionId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCInterpreterScore]') AND type in (N'U'))
DROP TABLE [dbo].[MCInterpreterScore]
GO
