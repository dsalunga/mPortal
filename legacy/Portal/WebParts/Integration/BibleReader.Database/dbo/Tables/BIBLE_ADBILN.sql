CREATE TABLE [dbo].[BIBLE_ADBILN] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ADB__BookC__46B27FE2] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ADB__Chapt__47A6A41B] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ADB__Verse__489AC854] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ADBILN] PRIMARY KEY CLUSTERED ([Id] ASC)
);

