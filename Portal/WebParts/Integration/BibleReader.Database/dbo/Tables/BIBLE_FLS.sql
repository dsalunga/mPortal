CREATE TABLE [dbo].[BIBLE_FLS] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_FLS__BookC__078C1F06] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_FLS__Chapt__0880433F] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_FLS__Verse__09746778] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_FLS] PRIMARY KEY CLUSTERED ([Id] ASC)
);

