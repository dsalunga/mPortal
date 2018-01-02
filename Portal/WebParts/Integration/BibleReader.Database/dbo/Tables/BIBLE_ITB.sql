CREATE TABLE [dbo].[BIBLE_ITB] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ITB__BookC__1B9317B3] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ITB__Chapt__1C873BEC] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ITB__Verse__1D7B6025] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ITB] PRIMARY KEY CLUSTERED ([Id] ASC)
);

