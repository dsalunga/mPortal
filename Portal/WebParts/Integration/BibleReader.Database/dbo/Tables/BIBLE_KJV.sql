CREATE TABLE [dbo].[BIBLE_KJV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF_BibleKJV_BookCode] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF_BibleKJV_ChapterId] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF_BibleKJV_VerseId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BibleKJV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

