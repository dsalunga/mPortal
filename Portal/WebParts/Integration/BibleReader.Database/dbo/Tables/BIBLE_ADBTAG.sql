CREATE TABLE [dbo].[BIBLE_ADBTAG] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ADB__BookC__2BFE89A6] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ADB__Chapt__2CF2ADDF] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ADB__Verse__2DE6D218] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ADBTAG] PRIMARY KEY CLUSTERED ([Id] ASC)
);

