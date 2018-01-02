CREATE TABLE [dbo].[BIBLE_ABTAG] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ABT__BookC__05D8E0BE] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ABT__Chapt__06CD04F7] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ABT__Verse__07C12930] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ABTAG] PRIMARY KEY CLUSTERED ([Id] ASC)
);

