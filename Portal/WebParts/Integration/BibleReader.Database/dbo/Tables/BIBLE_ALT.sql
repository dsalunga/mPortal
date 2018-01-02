CREATE TABLE [dbo].[BIBLE_ALT] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ALT__BookC__681373AD] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ALT__Chapt__690797E6] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ALT__Verse__69FBBC1F] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ALT] PRIMARY KEY CLUSTERED ([Id] ASC)
);

