CREATE TABLE [dbo].[BIBLE_PJFA] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_PJF__BookC__2F9A1060] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_PJF__Chapt__308E3499] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_PJF__Verse__318258D2] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_PJFA] PRIMARY KEY CLUSTERED ([Id] ASC)
);

