CREATE TABLE [dbo].[BIBLE_SF] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_SF__BookCo__3552E9B6] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_SF__Chapte__36470DEF] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_SF__VerseC__373B3228] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_SF] PRIMARY KEY CLUSTERED ([Id] ASC)
);

