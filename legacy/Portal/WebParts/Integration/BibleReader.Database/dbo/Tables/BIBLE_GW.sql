CREATE TABLE [dbo].[BIBLE_GW] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_GW__BookCo__12FDD1B2] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_GW__Chapte__13F1F5EB] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_GW__VerseC__14E61A24] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_GW] PRIMARY KEY CLUSTERED ([Id] ASC)
);

