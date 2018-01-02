CREATE TABLE [dbo].[BIBLE_ADBCEB] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ADB__BookC__3F115E1A] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ADB__Chapt__40058253] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ADB__Verse__40F9A68C] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ADBCEB] PRIMARY KEY CLUSTERED ([Id] ASC)
);

