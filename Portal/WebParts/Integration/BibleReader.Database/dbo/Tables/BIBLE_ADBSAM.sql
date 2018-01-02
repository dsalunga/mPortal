CREATE TABLE [dbo].[BIBLE_ADBSAM] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ADB__BookC__4E53A1AA] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ADB__Chapt__4F47C5E3] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ADB__Verse__503BEA1C] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ADBSAM] PRIMARY KEY CLUSTERED ([Id] ASC)
);

