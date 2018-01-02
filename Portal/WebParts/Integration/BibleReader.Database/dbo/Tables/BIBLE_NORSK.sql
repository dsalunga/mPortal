CREATE TABLE [dbo].[BIBLE_NORSK] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_NOR__BookC__2CBDA3B5] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_NOR__Chapt__2DB1C7EE] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_NOR__Verse__2EA5EC27] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_NORSK] PRIMARY KEY CLUSTERED ([Id] ASC)
);

