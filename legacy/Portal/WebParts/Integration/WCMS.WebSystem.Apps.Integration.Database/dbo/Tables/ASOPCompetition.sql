CREATE TABLE [dbo].[MusicCompetition] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (500)  NOT NULL,
    [Judges]            NVARCHAR (1000) CONSTRAINT [DF__MCCompe__Judges] DEFAULT ('') NOT NULL,
    [ScoreLocked]       INT             CONSTRAINT [DF__MCCompe__ScoreLocked] DEFAULT ((0)) NOT NULL,
    [CompetitionDate]   DATETIME        CONSTRAINT [DF__MCCompe__CompetitionDate] DEFAULT (getdate()) NOT NULL,
    [VoteLocked]        INT             CONSTRAINT [DF_MCComp_VoteLocked] DEFAULT ((0)) NOT NULL,
    [VoteMasked]        INT             CONSTRAINT [DF_MCComp_VoteMasked] DEFAULT ((0)) NOT NULL,
    [PeoplesChoiceId]   INT             CONSTRAINT [DF_MCComp_PeoplesChoiceId] DEFAULT ((-1)) NOT NULL,
    [BestInterpreterId] INT             CONSTRAINT [DF_MCComp_BestInterpreterId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_MusicCompetition] PRIMARY KEY CLUSTERED ([Id] ASC)
);



