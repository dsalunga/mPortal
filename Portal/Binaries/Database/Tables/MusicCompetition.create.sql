SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MusicCompetition]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MusicCompetition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Judges] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ScoreLocked] [int] NOT NULL,
	[CompetitionDate] [datetime] NOT NULL,
	[VoteLocked] [int] NOT NULL,
	[VoteMasked] [int] NOT NULL,
	[PeoplesChoiceId] [int] NOT NULL,
	[BestInterpreterId] [int] NOT NULL,
 CONSTRAINT [PK_MusicCompetition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCCompe__Judges]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] ADD  CONSTRAINT [DF__MCCompe__Judges]  DEFAULT ('') FOR [Judges]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCCompe__ScoreLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] ADD  CONSTRAINT [DF__MCCompe__ScoreLocked]  DEFAULT ((0)) FOR [ScoreLocked]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCCompe__CompetitionDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] ADD  CONSTRAINT [DF__MCCompe__CompetitionDate]  DEFAULT (getdate()) FOR [CompetitionDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCComp_VoteLocked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] ADD  CONSTRAINT [DF_MCComp_VoteLocked]  DEFAULT ((0)) FOR [VoteLocked]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCComp_VoteMasked]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] ADD  CONSTRAINT [DF_MCComp_VoteMasked]  DEFAULT ((0)) FOR [VoteMasked]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCComp_PeoplesChoiceId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] ADD  CONSTRAINT [DF_MCComp_PeoplesChoiceId]  DEFAULT ((-1)) FOR [PeoplesChoiceId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCComp_BestInterpreterId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicCompetition] ADD  CONSTRAINT [DF_MCComp_BestInterpreterId]  DEFAULT ((-1)) FOR [BestInterpreterId]
END

GO
