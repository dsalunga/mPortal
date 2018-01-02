CREATE TABLE [dbo].[BIBLE_NIV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_NIV__BookC__29E1370A] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_NIV__Chapt__2AD55B43] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_NIV__Verse__2BC97F7C] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_NIV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

