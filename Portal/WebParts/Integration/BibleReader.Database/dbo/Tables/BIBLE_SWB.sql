CREATE TABLE [dbo].[BIBLE_SWB] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_SWB__BookC__3DE82FB7] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_SWB__Chapt__3EDC53F0] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_SWB__Verse__3FD07829] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_SWB] PRIMARY KEY CLUSTERED ([Id] ASC)
);

