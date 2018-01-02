SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCInterpreterScore]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MCInterpreterScore](
	[Id] [int] NOT NULL,
	[JudgeId] [int] NOT NULL,
	[VoiceQuality] [int] NOT NULL,
	[Interpretation] [int] NOT NULL,
	[StagePresence] [int] NOT NULL,
	[OverallImpact] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[CandidateId] [int] NOT NULL,
	[CompetitionId] [int] NOT NULL,
 CONSTRAINT [PK_MCInterpreterScore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCInter__DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCInterpreterScore] ADD  CONSTRAINT [DF__MCInter__DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCInter__CandidateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCInterpreterScore] ADD  CONSTRAINT [DF__MCInter__CandidateId]  DEFAULT ((-1)) FOR [CandidateId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCInter__CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCInterpreterScore] ADD  CONSTRAINT [DF__MCInter__CompetitionId]  DEFAULT ((-1)) FOR [CompetitionId]
END

GO
