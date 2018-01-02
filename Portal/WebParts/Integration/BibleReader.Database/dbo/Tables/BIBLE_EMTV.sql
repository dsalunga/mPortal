CREATE TABLE [dbo].[BIBLE_EMTV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_EMT__BookC__01D345B0] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_EMT__Chapt__02C769E9] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_EMT__Verse__03BB8E22] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_EMTV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

