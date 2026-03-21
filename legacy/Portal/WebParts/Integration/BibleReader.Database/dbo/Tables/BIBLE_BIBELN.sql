CREATE TABLE [dbo].[BIBLE_BIBELN] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_BIB__BookC__73852659] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_BIB__Chapt__74794A92] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_BIB__Verse__756D6ECB] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_BIBELN] PRIMARY KEY CLUSTERED ([Id] ASC)
);

