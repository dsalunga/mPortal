CREATE TABLE [dbo].[BIBLE_RSV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_RSV__BookC__32767D0B] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_RSV__Chapt__336AA144] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_RSV__Verse__345EC57D] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_RSV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

