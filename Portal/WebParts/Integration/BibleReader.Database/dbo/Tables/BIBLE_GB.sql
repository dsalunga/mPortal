CREATE TABLE [dbo].[BIBLE_GB] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_GB__BookCo__0A688BB1] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_GB__Chapte__0B5CAFEA] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_GB__VerseC__0C50D423] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_GB] PRIMARY KEY CLUSTERED ([Id] ASC)
);

