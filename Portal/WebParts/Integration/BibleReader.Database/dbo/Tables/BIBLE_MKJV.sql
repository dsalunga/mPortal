CREATE TABLE [dbo].[BIBLE_MKJV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_MKJ__BookC__2704CA5F] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_MKJ__Chapt__27F8EE98] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_MKJ__Verse__28ED12D1] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_MKJV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

