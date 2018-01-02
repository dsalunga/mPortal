CREATE TABLE [dbo].[BIBLE_VULGATE] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_VUL__BookC__40C49C62] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_VUL__Chapt__41B8C09B] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_VUL__Verse__42ACE4D4] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_VULGATE] PRIMARY KEY CLUSTERED ([Id] ASC)
);

