SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCSongScore]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MCSongScore](
	[Id] [int] NOT NULL,
	[JudgeId] [int] NOT NULL,
	[Musicality] [int] NOT NULL,
	[LyricsMessage] [int] NOT NULL,
	[OverallImpact] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[CandidateId] [int] NOT NULL,
	[CompetitionId] [int] NOT NULL,
 CONSTRAINT [PK_MCSongScore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCSongS__DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCSongScore] ADD  CONSTRAINT [DF__MCSongS__DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCSongS__CandidateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCSongScore] ADD  CONSTRAINT [DF__MCSongS__CandidateId]  DEFAULT ((-1)) FOR [CandidateId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCSongS__CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCSongScore] ADD  CONSTRAINT [DF__MCSongS__CompetitionId]  DEFAULT ((-1)) FOR [CompetitionId]
END

GO
