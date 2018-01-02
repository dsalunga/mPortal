CREATE TABLE [dbo].[BIBLE_LITV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_LIT__BookC__214BF109] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_LIT__Chapt__22401542] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_LIT__Verse__2334397B] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_LITV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

