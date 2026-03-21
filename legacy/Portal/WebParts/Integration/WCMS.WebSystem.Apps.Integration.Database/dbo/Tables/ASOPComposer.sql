CREATE TABLE [dbo].[MCComposer] (
    [Id]            INT            NOT NULL,
    [Name]          NVARCHAR (500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF__MCComposer__Name] DEFAULT ('') NOT NULL,
    [Entry]         NVARCHAR (MAX) COLLATE Latin1_General_CI_AI CONSTRAINT [DF__MCComposer__Entry] DEFAULT ('') NOT NULL,
    [Locale]        NVARCHAR (500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF__MCComposer__Locale] DEFAULT ('') NOT NULL,
    [Work]          NVARCHAR (500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF__MCComposer__Work] DEFAULT ('') NOT NULL,
    [Description]   NVARCHAR (MAX) COLLATE Latin1_General_CI_AI CONSTRAINT [DF__MCComposer__Description] DEFAULT ('') NOT NULL,
    [PhotoFile]     NVARCHAR (500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF__MCComposer__PhotoFile] DEFAULT ('') NOT NULL,
    [NickName]      NVARCHAR (500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF__MCComposer__NickName] DEFAULT ('') NOT NULL,
    [Active]        INT            CONSTRAINT [DF__MCComposer__Active] DEFAULT ((1)) NOT NULL,
    [CompetitionId] INT            CONSTRAINT [DF__MCComposer__CompetitionId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK__MCComposer_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);



