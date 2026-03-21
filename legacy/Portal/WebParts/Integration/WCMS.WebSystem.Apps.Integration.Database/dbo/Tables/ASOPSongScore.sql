CREATE TABLE [dbo].[MCSongScore] (
    [Id]            INT      NOT NULL,
    [JudgeId]       INT      NOT NULL,
    [Musicality]    INT      NOT NULL,
    [LyricsMessage] INT      NOT NULL,
    [OverallImpact] INT      NOT NULL,
    [DateModified]  DATETIME CONSTRAINT [DF__MCSongS__DateModified] DEFAULT (getdate()) NOT NULL,
    [CandidateId]   INT      CONSTRAINT [DF__MCSongS__CandidateId] DEFAULT ((-1)) NOT NULL,
    [CompetitionId] INT      CONSTRAINT [DF__MCSongS__CompetitionId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_MCSongScore] PRIMARY KEY CLUSTERED ([Id] ASC)
);



