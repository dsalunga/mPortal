CREATE TABLE [dbo].[BIBLE_WEB] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_WEB__BookC__43A1090D] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_WEB__Chapt__44952D46] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_WEB__Verse__4589517F] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_WEB] PRIMARY KEY CLUSTERED ([Id] ASC)
);

