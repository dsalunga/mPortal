CREATE TABLE [dbo].[BIBLE_SRV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_SRV__BookC__382F5661] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_SRV__Chapt__39237A9A] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_SRV__Verse__3A179ED3] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_SRV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

