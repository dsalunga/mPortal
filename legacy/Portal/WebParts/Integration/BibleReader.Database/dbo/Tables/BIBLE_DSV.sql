CREATE TABLE [dbo].[BIBLE_DSV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_DSV__BookC__7EF6D905] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_DSV__Chapt__7FEAFD3E] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_DSV__Verse__00DF2177] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_DSV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

