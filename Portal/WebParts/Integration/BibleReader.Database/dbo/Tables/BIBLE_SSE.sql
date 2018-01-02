CREATE TABLE [dbo].[BIBLE_SSE] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_SSE__BookC__3B0BC30C] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_SSE__Chapt__3BFFE745] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_SSE__Verse__3CF40B7E] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_SSE] PRIMARY KEY CLUSTERED ([Id] ASC)
);

