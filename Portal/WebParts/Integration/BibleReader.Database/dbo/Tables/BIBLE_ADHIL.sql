CREATE TABLE [dbo].[BIBLE_ADHIL] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ADH__BookC__65370702] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ADH__Chapt__662B2B3B] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ADH__Verse__671F4F74] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ADHIL] PRIMARY KEY CLUSTERED ([Id] ASC)
);

