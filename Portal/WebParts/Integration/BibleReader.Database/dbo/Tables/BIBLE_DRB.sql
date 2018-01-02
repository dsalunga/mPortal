CREATE TABLE [dbo].[BIBLE_DRB] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_DRB__BookC__7C1A6C5A] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_DRB__Chapt__7D0E9093] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_DRB__Verse__7E02B4CC] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_DRB] PRIMARY KEY CLUSTERED ([Id] ASC)
);

