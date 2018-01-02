CREATE TABLE [dbo].[BIBLE_GLB] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_GLB__BookC__0D44F85C] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_GLB__Chapt__0E391C95] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_GLB__Verse__0F2D40CE] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_GLB] PRIMARY KEY CLUSTERED ([Id] ASC)
);

