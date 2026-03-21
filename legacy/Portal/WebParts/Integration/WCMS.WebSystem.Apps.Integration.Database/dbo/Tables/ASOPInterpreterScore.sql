CREATE TABLE [dbo].[MCInterpreterScore] (
    [Id]             INT      NOT NULL,
    [JudgeId]        INT      NOT NULL,
    [VoiceQuality]   INT      NOT NULL,
    [Interpretation] INT      NOT NULL,
    [StagePresence]  INT      NOT NULL,
    [OverallImpact]  INT      NOT NULL,
    [DateModified]   DATETIME CONSTRAINT [DF__MCInter__DateModified] DEFAULT (getdate()) NOT NULL,
    [CandidateId]    INT      CONSTRAINT [DF__MCInter__CandidateId] DEFAULT ((-1)) NOT NULL,
    [CompetitionId]  INT      CONSTRAINT [DF__MCInter__CompetitionId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_MCInterpreterScore] PRIMARY KEY CLUSTERED ([Id] ASC)
);



