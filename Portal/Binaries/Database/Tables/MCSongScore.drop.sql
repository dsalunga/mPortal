IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCSongS__DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCSongScore] DROP CONSTRAINT [DF__MCSongS__DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCSongS__CandidateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCSongScore] DROP CONSTRAINT [DF__MCSongS__CandidateId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCSongS__CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCSongScore] DROP CONSTRAINT [DF__MCSongS__CompetitionId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCSongScore]') AND type in (N'U'))
DROP TABLE [dbo].[MCSongScore]
GO
