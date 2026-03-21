CREATE TABLE [dbo].[MCVote] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Code]          NVARCHAR (250) CONSTRAINT [DF_MCVote_Code] DEFAULT ('') NOT NULL,
    [FirstName]     NVARCHAR (250) CONSTRAINT [DF_MCVote_FirstName] DEFAULT ('') NOT NULL,
    [LastName]      NVARCHAR (250) CONSTRAINT [DF_MCVote_LastName] DEFAULT ('') NOT NULL,
    [MobileNumber]  NVARCHAR (250) CONSTRAINT [DF_MCVote_MobileNumber] DEFAULT ('') NOT NULL,
    [Email]         NVARCHAR (250) CONSTRAINT [DF_MCVote_Email] DEFAULT ('') NOT NULL,
    [CandidateId]   INT            CONSTRAINT [DF_MCVote_CandidateId] DEFAULT ((-1)) NOT NULL,
    [DateVoted]     DATETIME       NOT NULL,
    [UserName]      NVARCHAR (250) CONSTRAINT [DF_MCVote_UserName] DEFAULT ('') NOT NULL,
    [Status]        INT            CONSTRAINT [DF__MCVote__Status] DEFAULT ((0)) NOT NULL,
    [CompetitionId] INT            CONSTRAINT [DF__MCVote__CompetitionId] DEFAULT ((-1)) NOT NULL,
    [IPAddress]     NVARCHAR (50)  CONSTRAINT [DF__MCVote__IPAddress] DEFAULT ('') NOT NULL,
    [Spam]          INT            CONSTRAINT [DF__MCVote__Spam] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MCVote] PRIMARY KEY CLUSTERED ([Id] ASC)
);



