CREATE TABLE [dbo].[BIBLE_AOV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_AOV__BookC__6AEFE058] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_AOV__Chapt__6BE40491] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_AOV__Verse__6CD828CA] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_AOV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

