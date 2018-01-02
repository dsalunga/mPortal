CREATE TABLE [dbo].[BIBLE_ISV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ISV__BookC__18B6AB08] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ISV__Chapt__19AACF41] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ISV__Verse__1A9EF37A] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ISV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

