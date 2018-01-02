CREATE TABLE [dbo].[BIBLE_ASV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ASV__BookC__6DCC4D03] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ASV__Chapt__6EC0713C] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ASV__Verse__6FB49575] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ASV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

