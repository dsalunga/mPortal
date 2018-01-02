CREATE TABLE [dbo].[BIBLE_BBE] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_BBE__BookC__70A8B9AE] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_BBE__Chapt__719CDDE7] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_BBE__Verse__72910220] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_BBE] PRIMARY KEY CLUSTERED ([Id] ASC)
);

