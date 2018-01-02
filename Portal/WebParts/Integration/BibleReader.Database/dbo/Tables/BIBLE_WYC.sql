CREATE TABLE [dbo].[BIBLE_WYC] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_WYC__BookC__4959E263] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_WYC__Chapt__4A4E069C] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_WYC__Verse__4B422AD5] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_WYC] PRIMARY KEY CLUSTERED ([Id] ASC)
);

