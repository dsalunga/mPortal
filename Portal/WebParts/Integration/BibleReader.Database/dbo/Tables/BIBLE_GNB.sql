CREATE TABLE [dbo].[BIBLE_GNB] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_GNB__BookC__10216507] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_GNB__Chapt__11158940] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_GNB__Verse__1209AD79] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_GNB] PRIMARY KEY CLUSTERED ([Id] ASC)
);

