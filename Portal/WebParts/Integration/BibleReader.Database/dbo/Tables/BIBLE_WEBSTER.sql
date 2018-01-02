CREATE TABLE [dbo].[BIBLE_WEBSTER] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_WEB__BookC__467D75B8] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_WEB__Chapt__477199F1] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_WEB__Verse__4865BE2A] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_WEBSTER] PRIMARY KEY CLUSTERED ([Id] ASC)
);

