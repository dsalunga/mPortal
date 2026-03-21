CREATE TABLE [dbo].[MCCandidate] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (250)  CONSTRAINT [DF_MCCandidate_Name] DEFAULT ('') NOT NULL,
    [Entry]         NVARCHAR (2000) CONSTRAINT [DF_MCCandidate_Entry] DEFAULT ('') NOT NULL,
    [Lyrics]        NVARCHAR (MAX)  CONSTRAINT [DF_MCCandidate_Lyrics] DEFAULT ('') NOT NULL,
    [SourceUrl]     NVARCHAR (500)  CONSTRAINT [DF_MCCandidate_SourceUrl] DEFAULT ('') NOT NULL,
    [SourceUrl2]    NVARCHAR (500)  CONSTRAINT [DF_MCCandidate_SourceUrl2] DEFAULT ('') NOT NULL,
    [Lyricist]      NVARCHAR (250)  CONSTRAINT [DF_MCCandidate_Lyricist] DEFAULT ('') NOT NULL,
    [Interpreter]   NVARCHAR (250)  CONSTRAINT [DF_MCCandidate_Interpreter] DEFAULT ('') NOT NULL,
    [PhotoFile]     NVARCHAR (500)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_MCCandidate_PhotoFile] DEFAULT ('') NOT NULL,
    [CompetitionId] INT             CONSTRAINT [DF_MCCandidate_CompetitionId] DEFAULT ((-1)) NOT NULL,
    [Rank]          INT             CONSTRAINT [DF_MCCandidate_Rank] DEFAULT ((0)) NOT NULL,
    [WinnerRank]    INT             CONSTRAINT [DF_MCCandidate_WinnerRank] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MCCandidate] PRIMARY KEY CLUSTERED ([Id] ASC)
);



