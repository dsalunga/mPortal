CREATE TABLE [dbo].[BIBLE_YLT] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_YLT__BookC__4C364F0E] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_YLT__Chapt__4D2A7347] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_YLT__Verse__4E1E9780] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_YLT] PRIMARY KEY CLUSTERED ([Id] ASC)
);

