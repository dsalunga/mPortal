IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCCompe__Judges]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] DROP CONSTRAINT [DF__MCCompe__Judges]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCCompe__ScoreLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] DROP CONSTRAINT [DF__MCCompe__ScoreLocked]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCCompe__CompetitionDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] DROP CONSTRAINT [DF__MCCompe__CompetitionDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCComp_VoteLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] DROP CONSTRAINT [DF_MCComp_VoteLocked]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCComp_VoteMasked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] DROP CONSTRAINT [DF_MCComp_VoteMasked]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCComp_PeoplesChoiceId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] DROP CONSTRAINT [DF_MCComp_PeoplesChoiceId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCComp_BestInterpreterId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] DROP CONSTRAINT [DF_MCComp_BestInterpreterId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MusicCompetition]') AND type in (N'U'))
DROP TABLE [dbo].[MusicCompetition]
GO
